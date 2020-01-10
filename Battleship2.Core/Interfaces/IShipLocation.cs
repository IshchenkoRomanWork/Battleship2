using Battleship2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IShipLocation
    {
        public Direction Direction { get; set; }
        public (int, int) Coords {get; set; }
    }
}
