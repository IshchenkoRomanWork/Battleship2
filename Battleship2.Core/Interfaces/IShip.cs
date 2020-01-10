using Battleship2.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IShip
    {
        public ShipType Type { get; set; }
        public List<DeckState> DeckStates { get; set; }
    }
}
