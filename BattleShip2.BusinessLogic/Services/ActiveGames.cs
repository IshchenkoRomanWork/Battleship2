using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Services
{
    public class ActiveGames
    {
        public ConcurrentBag<Game> Games { get; set; }
        public ActiveGames()
        {
            Games = new ConcurrentBag<Game>();
        }
        public Guid CreateGame(Player creator)
        {
            var game = new Game();
            creator.CurrentMap = new Map();
            game.GameDetails.AddPlayer(creator);
            Games.Add(game);
            return game.Id;
        }
        public void ConnectToGame(Player connected, Guid gameId)
        {
            var game = Games.SingleOrDefault(game => game.Id == gameId);
            connected.CurrentMap = new Map();
            game.GameDetails.AddPlayer(connected);
        }
        public Game GetGame(Guid gameId)
        {
            return Games.SingleOrDefault(game => game.Id == gameId);
        }
        public Player GetPlayerById(Guid Id)
        {
            return Games.First(game => game.GameDetails.Players.Any(p => p.Id == Id)).GameDetails.Players.First(p => p.Id == Id);
        }
        public Game GetGameByPlayerId(Guid Id)
        {
            return Games.SingleOrDefault(game => game.GameDetails.Players.Any(p => p.Id == Id));
        }
    }
}
