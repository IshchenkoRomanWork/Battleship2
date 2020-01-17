using Battleship2.Core.Enums;
using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public GameDetails GameDetails { get; set; }
        public Player ActivePlayer { get; set; }
        public List<bool> PlayersReady {get; set;}

        public Game()
        {
            Id = Guid.NewGuid();
            GameDetails = new GameDetails()
            {
                Players = new List<Player>(),
                ShotList = new List<GameShot>()
            };
            PlayersReady = new List<bool> { false, false };
        }
        public bool PlayerIsReady(Player player)
        {
            bool fourDecksArePlaced = player.CurrentMap.ShipInformationList.Count(si => si.Ship.Type == ShipType.FourDeck) == 4;
            bool threeDecksArePlaced = player.CurrentMap.ShipInformationList.Count(si => si.Ship.Type == ShipType.ThreeDeck) == 3;
            bool twoDecksArePlaced = player.CurrentMap.ShipInformationList.Count(si => si.Ship.Type == ShipType.TwoDeck) == 2;
            bool oneDecksArePlaced = player.CurrentMap.ShipInformationList.Count(si => si.Ship.Type == ShipType.OneDeck) == 1;
            bool ready = fourDecksArePlaced && threeDecksArePlaced && twoDecksArePlaced && oneDecksArePlaced;
            if (ready)
            {
                PlayersReady[GameDetails.Players.IndexOf(player)] = true;
            }
            return ready;
        }
        public bool GameStart()
        {
            bool gameStarted = PlayersReady.All(playerReady => playerReady);
            if (gameStarted)
            {
                int firstPlayer = new Random().Next(1);
                ActivePlayer = GameDetails.Players.ElementAt(firstPlayer);
            }
            return gameStarted;
        }

        public void AddShip(Player currentPlayer, ShipInformation shipInfo)
        {
            currentPlayer.CurrentMap.AddShip(shipInfo);
        }
        public List<(Coords, bool)> ShotAt(Player targetPlayer, Coords coords)
        {
            var map = targetPlayer.CurrentMap;
            bool targetHit;
            bool shipIsDrown;
            var shotsResult = map.ShotAtCoords(coords, out targetHit, out shipIsDrown);
            GameDetails.ShotList.Add(new GameShot
            {
                Shooter = ActivePlayer,
                TargetCoords = coords,
                TargetHit = targetHit,
                ShipIsDrown = shipIsDrown
            });
            if (!targetHit)
            {
                ActivePlayer = targetPlayer;
                return shotsResult.Select(sr => (sr, false)).ToList();
            }
            else
            {
                return shotsResult.Select(sr => (sr, map.GetShipInformation(sr.CoordX, sr.CoordY) != null)).ToList();
            }
        }
        public bool CheckForWin()
        {
            var playersWithRemaininShips = GameDetails.Players.Where(player => !player.CurrentMap.ShipInformationList.
            Any(si => si.Ship.DeckStates.
            Any(ds => ds == DeckState.Undamaged))).ToList();
            if(playersWithRemaininShips.Count == 1)
            {
                GameDetails.PlayerMaps = GameDetails.Players.Select(pl => pl.CurrentMap).ToList();
                return true;
            }
            return false;
        }
    }
}
