using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class ShipLocation : Entity, IShipLocation
    {
        public Direction Direction { get; set; }
        public (int, int) Coords { get; set ; }
    }
}
