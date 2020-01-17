using Battleship2.Core.Enums;
using Battleship2.Core.Models;
using Battleship2.MVC.Models;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Battleship2.MVC.Hubs
{
    public class BattleShipHub : Hub
    {
        private UnitOfWork _unitOfWork { get; set; }
        private ActiveGames _activeGames { get; set; }
        private static ConcurrentDictionary<string, string> _connectionAndPlayerIds = new ConcurrentDictionary<string, string>();
        public BattleShipHub(UnitOfWork unitOfWork, ActiveGames activeGames, Helper helper)
        {
            _unitOfWork = unitOfWork;
            _activeGames = activeGames;
        }
        public void PlayerConnected(string playerId, string gameId)
        {
            var connectionId = Context.ConnectionId;
            Player player = _unitOfWork.GetPlayer(Guid.Parse(playerId));
            _activeGames.ConnectToGame(player, Guid.Parse(gameId));
            var game = _activeGames.GetGame(Guid.Parse(gameId));
            var opponent = game.GameDetails.Players.SingleOrDefault(p => p.Id != Guid.Parse(playerId));

            _connectionAndPlayerIds.AddOrUpdate(playerId, connectionId, (key, oldvalue) => Context.ConnectionId);
            Clients.Client(connectionId).SendAsync("OpponentConnected", opponent.Name);
            Clients.Client(_connectionAndPlayerIds.GetOrAdd(opponent.Id.ToString(), (key) => throw new Exception("ConnectionId non existent")))
                .SendAsync("OpponentConnected", player.Name);
        }
        public void GameCreated(string playerId)
        {
            Player player = _unitOfWork.GetPlayer(Guid.Parse(playerId));
            var gameid = _activeGames.CreateGame(player);
            _connectionAndPlayerIds.AddOrUpdate(playerId, Context.ConnectionId, (key, oldvalue) => Context.ConnectionId);
            Clients.Client(_connectionAndPlayerIds.GetOrAdd(playerId, (key) => throw new Exception("ConnectionId non existent")))
                .SendAsync("SetId", gameid);
        }
        public void AddShip(JsonElement[] sendData)
        {
            var connectionId = Context.ConnectionId;
            int headX = sendData[0].GetInt32();
            int headY = sendData[1].GetInt32();
            int tailX = sendData[2].GetInt32();
            int tailY = sendData[3].GetInt32();
            int length = sendData[4].GetInt32();
            Guid playerId = Guid.Parse(sendData[5].GetString());

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
            catch(Exception ex)
            {
                if(ex.Data["type"] != null && (GameException)ex.Data["type"] == GameException.ShipsTouch)
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
                dir = Direction.Left;
            }
            return dir;
        }
        public void Ready(string playerId)
        {
            var player = _activeGames.GetPlayerById(Guid.Parse(playerId));
            var game = _activeGames.GetGameByPlayerId(Guid.Parse(playerId));
            game.PlayerIsReady(player);
            if(game.PlayersReady.All(r => r))
            {
                foreach(var pl in game.GameDetails.Players)
                {
                    var playerConnectionId = _connectionAndPlayerIds.GetOrAdd(pl.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
                    Clients.Client(playerConnectionId).SendAsync("GameStart");
                }
            }
        }
        public void ShotAt(object[] sendData)
        {
            int headX = (int)sendData[0];
            int headY = (int)sendData[1];
            string playerId = (string)sendData[1];
            var player = _activeGames.GetPlayerById(Guid.Parse(playerId));
            var game = _activeGames.GetGameByPlayerId(Guid.Parse(playerId));
            var opponent = game.GameDetails.Players.SingleOrDefault(p => p.Id != player.Id);
            var shotResult = game.ShotAt(opponent, new Coords(headX, headY));
            var shotInfo = game.GameDetails.ShotList.Last();
            var jsonShotResult = JsonConvert.SerializeObject(shotResult);
            var playerConnectionId = _connectionAndPlayerIds.GetOrAdd(player.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
            var opponentConnectionId = _connectionAndPlayerIds.GetOrAdd(opponent.Id.ToString(), (key) => throw new Exception("ConnectionId non existent"));
            Clients.Client(playerConnectionId).SendAsync("YourShootResult", jsonShotResult, shotInfo);
            Clients.Client(opponentConnectionId).SendAsync("YourFieldShooted", jsonShotResult, shotInfo);
            if(shotInfo.TargetHit)
            {
                Clients.Client(opponentConnectionId).SendAsync("YourTurn");
            }
            if(game.CheckForWin())
            {
                Win(game, playerConnectionId, opponentConnectionId);
            }
        }
        public void Win(Game game, string winnerConnectionId, string loserConnectionId)
        {
            _unitOfWork.AddGameDetails(game.GameDetails);
            Clients.Client(winnerConnectionId).SendAsync("GameEnded", true);
            Clients.Client(loserConnectionId).SendAsync("GameEnded", false);
        }
    }
}
