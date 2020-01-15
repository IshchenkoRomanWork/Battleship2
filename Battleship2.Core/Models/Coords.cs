using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class Coords : Entity
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public Coords() : base() { }
        public Coords(int coordX, int coordY)
        {
            CoordX = coordX;
            CoordY = coordY;
        }
    }
}
