using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class ShipInformation : Entity
    {
        public Ship Ship { get; set; }
        public ShipLocation Location { get; set; }
    }
}
