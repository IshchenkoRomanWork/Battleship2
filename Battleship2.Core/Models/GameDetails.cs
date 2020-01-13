using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class GameDetails : Entity
    {
        public ICollection<Player> Players { get; set; }
        public ICollection<Map> PlayerMaps { get; set; }
        public ICollection<GameShot> ShotList { get; set; }
    }
}
