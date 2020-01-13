using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class GameShot : Entity
    {
        public (int, int) TargetCoords { get; set; }
        public int PlayerNumber { get; set; }
        public bool TargetHit { get; set; }
        public bool ShipIsDrown { get; set; }
    }
}
