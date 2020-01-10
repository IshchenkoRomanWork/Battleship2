using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class Ship : IShip
    {
        public ShipType Type { get; set; }
        public List<DeckState> DeckStates { get; set; }
        public Guid Id { get; }

        public Ship(ShipType type)
        {

        }

        public IShip CreateShip(ShipType type)
        {
            return new Ship(type);
        }
    }
}
