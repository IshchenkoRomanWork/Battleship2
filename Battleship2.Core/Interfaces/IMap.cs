using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IMap
    {
        public void AddShip(IShipInformation shipInformation);
        public IShipInformation GetShipInformation(int xCoord, int yCoord);
    }
}
