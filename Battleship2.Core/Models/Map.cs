using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship2.Core.Models
{
    public class Map : Entity
    {
        private List<ShipInformation> _shipInformationList;
        private List<(int, int)> _occupiedCoords;
        public List<ShipInformation> ShipInformationList { 
            get
            {
                List<ShipInformation> copy = new List<ShipInformation>(_shipInformationList);
                return copy;
            }
            set
            {
                var newOccupiedCoords = new List<(int, int)>();
                foreach(var si in value)
                {
                    var coords = GetCoordsFromShipInformation(si);
                    ValidateCoords(coords);
                    newOccupiedCoords.AddRange(coords);
                }
                _shipInformationList = value;
                _occupiedCoords = newOccupiedCoords;
            }
        } 
        public List<(int, int)> ShotCoords { get; set; }
        public Map() : base()
        {
            _shipInformationList = new List<ShipInformation>();
            _occupiedCoords = new List<(int, int)>();
        }
        public void AddShip(ShipInformation shipInformation)
        {
            var shipCoords = GetCoordsFromShipInformation(shipInformation);
            ValidateCoords(shipCoords);

            _shipInformationList.Add(shipInformation);
            _occupiedCoords.AddRange(shipCoords);
        }
        public ShipInformation GetShipInformation(int xCoord, int yCoord)
        {
            return _shipInformationList.FirstOrDefault(si => GetCoordsFromShipInformation(si).
                                            Any(coord => coord == (xCoord, yCoord)));   
        }
        private void ValidateCoords(List<(int, int)> validationCoords)
        {
            CheckNotGoBeyondBorders(validationCoords);
            CheckCoordsAreFree(validationCoords);
            CheckCoordIsNotOnAxis(validationCoords[0]);
        }
        private void CheckNotGoBeyondBorders(List<(int, int)> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (Math.Abs(coord.Item1) > 10 || Math.Abs(coord.Item2) > 10)
                {
                    throw new Exception("Start coord can't lie on axis");
                }
            }
        } //Validation Check
        private void CheckCoordsAreFree(List<(int, int)> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (_occupiedCoords.Contains(coord))
                {
                    throw new Exception("There's another ship on this coordinates");
                }
            }
        } //Validation Check
        private void CheckCoordIsNotOnAxis((int, int) checkedCoord)
        {
            if (checkedCoord.Item1 != 0 && checkedCoord.Item2 != 0)
            {
                throw new Exception("There's another ship on this coordinates");
            }
        } //Validation Check
        private List<(int, int)> GetCoordsFromShipInformation(ShipInformation shipInformation)
        {
            int headX = shipInformation.Location.Coords.Item1;
            int headY = shipInformation.Location.Coords.Item2;
            int length = (int)shipInformation.Ship.Type;
            var sectionCoords = new List<(int, int)>();
            sectionCoords.Add((headX, headY));

            for (int i = 1; i < length; i++)
            {
                switch (shipInformation.Location.Direction)
                {
                    case Direction.Right:
                        IsZero(headX - i, ref length, ref i);
                        sectionCoords.Add((headX - i, headY));
                        break;
                    case Direction.Left:
                        IsZero(headX + i, ref length, ref i);
                        sectionCoords.Add((headX + i, headY));
                        break;
                    case Direction.Down:
                        IsZero(headY + i, ref length, ref i);
                        sectionCoords.Add((headX, headY + i));
                        break;
                    case Direction.Up:
                        IsZero(headY - i, ref length, ref i);
                        sectionCoords.Add((headX, headY - i));
                        break;
                }
            }
            return sectionCoords;
        }
        private void IsZero(int value, ref int length, ref int i)
        {
            if (value == 0)
            {
                i++;
                length++;
            }
        }
    }
}
