using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public GameDetails GameDetails { get; set; }
        public Player ActivePlayer { get; set; }
        public ICollection<bool> PlayersReady {get; set;}
        public void GameStart()
        {

        }
        public void ShotAt(Player target, (int, int) coords)
        {

        }
        private Player CheckForWin()
        {
            return default;
        }
    }
}
