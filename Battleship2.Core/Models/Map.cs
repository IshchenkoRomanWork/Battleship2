using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class Map : IMap
    {
        public Guid Id { get; set; }

        public void AddShip(IShipInformation shipInformation)
        {
            throw new NotImplementedException();
        }

        public IShipInformation GetShipInformation(int xCoord, int yCoord)
        {
            throw new NotImplementedException();
        }
    }
}
