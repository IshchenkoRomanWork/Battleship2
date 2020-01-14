using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Services
{
    public class ActiveGames
    {
        public ICollection<Game> Games { get; set; }
        public void CreateGame(Player creator) { }
        public void ConnectToGame(Player connected) { }
        public Game GetGame(Guid gameId) { return default;  }
    }
}
