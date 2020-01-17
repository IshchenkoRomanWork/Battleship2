using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class GameDetails : Entity
    {
        public List<Player> Players { get; set; }
        public List<Map> PlayerMaps { get; set; }
        public List<GameShot> ShotList { get; set; }
    }
}
