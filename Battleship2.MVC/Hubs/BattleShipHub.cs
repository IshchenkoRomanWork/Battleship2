using Battleship2.Core.Enums;
using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;

namespace Battleship2.MVC.Hubs
{
    public class BattleShipHub : Hub
    {
        private UnitOfWork _unitOfWork { get; set; }
        private ActiveGames _activeGames { get; set; }
        private static ConcurrentDictionary<string, string> _connectionAndPlayerIds = new ConcurrentDictionary<string, string>();
        public BattleShipHub(UnitOfWork unitOfWork, ActiveGames activeGames)
        {
            _unitOfWork = unitOfWork;
            _activeGames = activeGames;
        }
        public void PlayerConnected(string playerId, string gameId)
        {
            var connectionId = Context.ConnectionId;
            Player player = _unitOfWork.GetPlayer(Int32.Parse(playerId));
            _activeGames.ConnectToGame(player, Int32.Parse(gameId));
            var game = _activeGames.GetGame(Int32.Parse(gameId));
            var opponent = game.GameDetails.Players.SingleOrDefault(p => p.Id != Int32.Parse(playerId));

            _connectionAndPlayerIds.AddOrUpdate(playerId, connectionId, (key, oldvalue) => Context.ConnectionId);
            Clients.Client(connectionId).SendAsync("OpponentConnected", opponent.Name);
            Clients.Client(_connectionAndPlayerIds.GetOrAdd(opponent.Id.ToString(), (key) => throw new Exception("ConnectionId non existent")))
                .SendAsync("OpponentConnected", player.Name);
        }
        public void GameCreated(string playerId)
        {
            Player player = _unitOfWork.GetPlayer(Int32.Parse(playerId));
            var gameid = _activeGames.CreateGame(player);
            _connectionAndPlayerIds.AddOrUpdate(playerId, Context.ConnectionId, (key, oldvalue) => Context.ConnectionId);
            Clients.Client(_connectionAndPlayerIds.GetOrAdd(playerId, (key) => throw new Exception("ConnectionId non existent")))
                .SendAsync("SetId", gameid.ToString());
        }
        public void AddShip(JsonElement[] sendData)
        {
            var connectionId = Context.ConnectionId;
            int headX = sendData[0].GetInt32();
            int headY = sendData[1].GetInt32();
            int tailX = sendData[2].GetInt32();
            int tailY = sendData[3].GetInt32();
            int length = sendData[4].GetInt32();
            int playerId = Int32.Parse(sendData[5].GetString());

            Game game = _activeGames.GetGameByPlayerId(playerId);
            Player player = _activeGames.GetPlayerById(playerId);
            var head = new Coords(headX, headY);
            var shipInfo = new ShipInformation()
            {
                Location = new ShipLocation()
                {
                    Coords = head,
                    Direction = GetDirection(head, new Coords(tailX, tailY))
                },
                Ship = new Ship(length)
            };
            bool addedsuccessfull = true;
            try
            {
                game.AddShip(player, shipInfo);
            }
            catch (Exception ex)
            {
                if (ex.Data["type"] != null && (GameException)ex.Data["type"] == GameException.ShipsTouch)
                {
                    Clients.Client(connectionId).SendAsync("ShipsCantTouch");
                }
                addedsuccessfull = false;
            }
            if (addedsuccessfull)
            {
                var jsonCoordList = JsonConvert.SerializeObject(shipInfo.GetCoordsFromShipInformation());
                Clients.Client(connectionId).SendAsync("AddShip", jsonCoordList, length);
            }
        }
        public void Ready(string playerId)
        {
            var player = _activeGames.GetPlayerById(Int32.Parse(playerId));
            var game = _activeGames.GetGameByPlayerId(Int32.Parse(playerId));
            game.PlayerIsReady(player);
            if (game.PlayersReady.All(r => r))
            {
                foreach (var pl in game.GameDetails.Players)
                {
                    var playerConnectionId = _connectionAndPlayerIds.GetOrAdd(pl.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
                    Clients.Client(playerConnectionId).SendAsync("GameStart");
                }
                game.GameStart();
                var firstPlayer = game.ActivePlayer;
                var firstPlayerConnectionId = _connectionAndPlayerIds.GetOrAdd(firstPlayer.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
                Clients.Client(firstPlayerConnectionId).SendAsync("YourTurn");
            }
        }
        public void PlayerLeft(string playerId)
        {
            var player = _activeGames.GetPlayerById(Int32.Parse(playerId));
            var game = _activeGames.GetGameByPlayerId(Int32.Parse(playerId));
            if (game != null)
            {
                game.GameDetails.Players.RemoveAll(p => p.Id == player.Id);
                if (game.GameDetails.Players.Count == 0)
                {
                    _activeGames.Games = new ConcurrentBag<Game>(_activeGames.Games.Where(g => g.Id != game.Id));
                }
                else
                {
                    foreach (var pl in game.GameDetails.Players)
                    {
                        var connectionId = _connectionAndPlayerIds.GetOrAdd(pl.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
                        Clients.Client(connectionId).SendAsync("OpponentLeft");
                    }
                }
            }
        }
        public void ShotAt(JsonElement[] sendData)
        {
            int headX = sendData[0].GetInt32();
            int headY = sendData[1].GetInt32();
            string playerId = sendData[2].GetString();
            var player = _activeGames.GetPlayerById(Int32.Parse(playerId));
            var game = _activeGames.GetGameByPlayerId(Int32.Parse(playerId));
            var opponent = game.GameDetails.Players.SingleOrDefault(p => p.Id != player.Id);
            var shotResult = game.ShotAt(opponent, new Coords(headX, headY));
            var shotInfo = game.GameDetails.ShotList.Last();
            var jsonShotResult = JsonConvert.SerializeObject(shotResult);
            var playerConnectionId = _connectionAndPlayerIds.GetOrAdd(player.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
            var opponentConnectionId = _connectionAndPlayerIds.GetOrAdd(opponent.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
            Clients.Client(playerConnectionId).SendAsync("YourShootResult", jsonShotResult, shotInfo.ToString());
            Clients.Client(opponentConnectionId).SendAsync("YourFieldShooted", jsonShotResult, shotInfo.ToString());
            if (!shotInfo.TargetHit)
            {
                game.ActivePlayer = opponent;
                Clients.Client(opponentConnectionId).SendAsync("YourTurn");
            }
            if (game.CheckForWin())
            {
                Win(game, playerConnectionId, opponentConnectionId, player);
            }
        }
        public void Win(Game game, string winnerConnectionId, string loserConnectionId, Player winner)
        {
            _unitOfWork.AddGameDetails(game.GameDetails);
            _unitOfWork.AddStatistics(new StatisticsItem()
            {
                GameDate = DateTime.Now,
                RemainingShips = winner.CurrentMap.ShipInformationList.Where(si => si.Ship.DeckStates.Any(ds => ds == DeckState.Undamaged)).Count(),
                GameTurnNumber = game.GameDetails.ShotList.Count,
                WinnerName = winner.Name
            });
            Clients.Client(winnerConnectionId).SendAsync("GameEnded", true);
            Clients.Client(loserConnectionId).SendAsync("GameEnded", false);
        }
        private Direction GetDirection(Coords head, Coords tail)
        {
            Direction dir = Direction.Up;
            if (head.CoordX > tail.CoordX)
            {
                dir = Direction.Right;
            }
            if (head.CoordX < tail.CoordX)
            {
                dir = Direction.Left;
            }
            if (head.CoordY > tail.CoordY)
            {
                dir = Direction.Up;
            }
            if (head.CoordY < tail.CoordY)
            {
                dir = Direction.Down;
            }
            return dir;
        }
    }
}
