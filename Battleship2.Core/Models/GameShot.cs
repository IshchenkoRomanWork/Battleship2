using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class GameShot : Entity
    {
        public Coords TargetCoords { get; set; }
        public Player Shooter { get; set; }
        public bool TargetHit { get; set; }
        public bool ShipIsDrown { get; set; }

        public override string ToString()
        {
            string value = string.Format("Player {0} shooted at {1}, {2}.", Shooter.Name, TargetCoords.CoordX, TargetCoords.CoordY);
            if(TargetHit)
            {
                value += " Ship hit!";
            }
            if (ShipIsDrown)
            {
                value += " Ship and drown!";
            }
            return value;

        }
    }
}
