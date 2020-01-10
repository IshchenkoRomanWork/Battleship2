using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship2.Core.Models
{
    class Map : Entity, IMap
    {
        private List<IShipInformation> _shipInformationList;
        private List<(int, int)> _occupiedCoords;
        public Map() : base()
        {
            _shipInformationList = new List<IShipInformation>();
            _occupiedCoords = new List<(int, int)>();
        }
        public void AddShip(IShipInformation shipInformation)
        {
            var shipCoords = GetCoordsFromShipInformation(shipInformation);
            ValidateCoords(shipCoords);

            _shipInformationList.Add(shipInformation);
            _occupiedCoords.AddRange(shipCoords);
        }
        public IShipInformation GetShipInformation(int xCoord, int yCoord)
        {
            return _shipInformationList.FirstOrDefault(si => GetCoordsFromShipInformation(si).
                                            Any(coord => coord == (xCoord, yCoord)));   
        }
        private void ValidateCoords(List<(int, int)> validationCoords)
        {
            CheckCoordsAreFree(validationCoords);
            CheckNotGoBeyondBorders(validationCoords);
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
        private List<(int, int)> GetCoordsFromShipInformation(IShipInformation shipInformation)
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
