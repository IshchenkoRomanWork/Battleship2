using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class ShipInformation : Entity, IShipInformation
    {
        public IShip Ship { get; set; }
        public IShipLocation Location { get; set; }
    }
}
