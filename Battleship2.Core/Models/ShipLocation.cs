using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class ShipLocation : Entity
    {
        public Direction Direction { get; set; }
        public Coords Coords { get; set ; }
    }
}
