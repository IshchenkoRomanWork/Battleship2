using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class Ship : Entity, IShip
    {
        public ShipType Type { get; set; }
        public List<DeckState> DeckStates { get; set; }
    }
}
