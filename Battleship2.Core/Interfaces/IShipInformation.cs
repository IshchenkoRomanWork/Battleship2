using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IShipInformation
    {
        IShip Ship { get; set; }
        IShipLocation Location  { get; set; }
    }
}
