using Battleship2.Core.Enums;
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
        public List<Coords> GetCoordsFromShipInformation()
        {
            int headX = Location.Coords.CoordX;
            int headY = Location.Coords.CoordY;
            var headCoords = new Coords(headX, headY);
            int length = (int)Ship.Type;
            var sectionCoords = new List<Coords>();
            sectionCoords.Add(headCoords);

            for (int i = 1; i < length; i++)
            {
                switch (Location.Direction)
                {
                    case Direction.Right:
                        sectionCoords.Add(new Coords(headX - 1, headY));
                        break;
                    case Direction.Left:
                        sectionCoords.Add(new Coords(headX + i, headY));
                        break;
                    case Direction.Down:
                        sectionCoords.Add(new Coords(headX, headY + i));
                        break;
                    case Direction.Up:
                        sectionCoords.Add(new Coords(headX, headY - i));
                        break;
                }
            }
            return sectionCoords;
        }
    }
}
