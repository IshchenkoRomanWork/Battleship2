using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Services
{
    public class ActiveGames
    {
        public ICollection<Game> Games { get; set; }
        public ActiveGames()
        {
            Games = new List<Game>();
        }
        public void CreateGame(Player creator)
        {
            var game = new Game();
            game.GameDetails.Players.Add(creator);
            game.GameDetails.PlayerMaps.Add(creator.CurrentMap);
            Games.Add(game);
        }
        public void ConnectToGame(Player connected, Guid gameId)
        {
            var game = Games.SingleOrDefault(game => game.Id == gameId);
            game.GameDetails.Players.Add(connected);
            connected.CurrentMap = new Map();
            game.GameDetails.PlayerMaps.Add(connected.CurrentMap);
        }
        public Game GetGame(Guid gameId)
        {
            return Games.SingleOrDefault(game => game.Id == gameId);
        }
    }
}
