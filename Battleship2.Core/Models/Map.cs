﻿using Battleship2.Core.Enums;
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
        private List<Coords> _occupiedCoords;
        public List<ShipInformation> ShipInformationList { 
            get
            {
                List<ShipInformation> copy = new List<ShipInformation>(_shipInformationList);
                return copy;
            }
            set
            {
                var newOccupiedCoords = new List<Coords>();
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
        public List<Coords> ShotCoords { get; set; }
        public Map() : base()
        {
            _shipInformationList = new List<ShipInformation>();
            _occupiedCoords = new List<Coords>();
        }
        public void AddShip(ShipInformation shipInformation)
        {
            var shipCoords = GetCoordsFromShipInformation(shipInformation);
            ValidateCoords(shipCoords);

            _shipInformationList.Add(shipInformation);
            _occupiedCoords.AddRange(shipCoords);
        }
        public void ShotAtCoords(Coords coords, out bool targetHit, out bool shipIsDrown)
        {
            targetHit = false;
            shipIsDrown = false;
            var shipInfo = GetShipInformation(coords.CoordX, coords.CoordY);
            if(shipInfo != null)
            {
                var section = GetCoordsFromShipInformation(shipInfo);
                int deckNumber = 1;
                for(; deckNumber < 4; deckNumber++)
                {
                    if(section[deckNumber-1] == coords)
                    {
                        break;
                    }
                }
                shipInfo.Ship.DeckStates[deckNumber - 1] = DeckState.Damaged;
                targetHit = true;
                shipIsDrown = shipInfo.Ship.DeckStates.All(ds => ds == DeckState.Damaged);
                if(shipIsDrown)
                {
                    ShotCoords.AddRange(GetSurroundingCoords(section));
                }
            }
            ShotCoords.Add(coords);
        }
        public ShipInformation GetShipInformation(int xCoord, int yCoord)
        {
            return _shipInformationList.FirstOrDefault(si => GetCoordsFromShipInformation(si).
                                            Any(coord => coord.CoordX == xCoord && coord.CoordY == yCoord));   
        }
        private void ValidateCoords(List<Coords> validationCoords)
        {
            CheckNotGoBeyondBorders(validationCoords);
            CheckCoordsAreFree(validationCoords);
            CheckCoordIsNotOnAxis(validationCoords[0]);
            CheckShipIsntNearAnother(validationCoords);
        }
        private void CheckNotGoBeyondBorders(List<Coords> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (Math.Abs(coord.CoordX) > 10 || Math.Abs(coord.CoordY) > 10)
                {
                    throw new Exception("Ship can't go beyond borders");
                }
            }
        } //Validation Check
        private void CheckCoordsAreFree(List<Coords> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (_occupiedCoords.Contains(coord))
                {
                    throw new Exception("There's another ship on this coordinates");
                }
            }
        } //Validation Check
        private void CheckCoordIsNotOnAxis(Coords checkedCoord)
        {
            if (checkedCoord.CoordX == 0 || checkedCoord.CoordY == 0)
            {
                throw new Exception("Start coord can't lie on axis");
            }
        } //Validation Check
        private void CheckShipIsntNearAnother(List<Coords> checkedCoords) 
        {
            var surroundingCoords = GetSurroundingCoords(checkedCoords);
            if (_occupiedCoords.Intersect(surroundingCoords).Any())
            {
                throw new Exception("Ships can't touch with angles or sides");
            }
        } //Validation Check
        private List<Coords> GetCoordsFromShipInformation(ShipInformation shipInformation)
        {
            int headX = shipInformation.Location.Coords.CoordX;
            int headY = shipInformation.Location.Coords.CoordY;
            var headCoords = new Coords(headX, headY);
            int length = (int)shipInformation.Ship.Type;
            var sectionCoords = new List<Coords>();
            sectionCoords.Add(headCoords);

            for (int i = 1; i < length; i++)
            {
                switch (shipInformation.Location.Direction)
                {
                    case Direction.Right:
                        IsZero(headX - i, ref length, ref i);
                        sectionCoords.Add(new Coords(headX -1 , headY));
                        break;
                    case Direction.Left:
                        IsZero(headX + i, ref length, ref i);
                        sectionCoords.Add(new Coords(headX + i, headY));
                        break;
                    case Direction.Down:
                        IsZero(headY + i, ref length, ref i);
                        sectionCoords.Add(new Coords(headX, headY + i));
                        break;
                    case Direction.Up:
                        IsZero(headY - i, ref length, ref i);
                        sectionCoords.Add(new Coords(headX, headY - i));
                        break;
                }
            }
            return sectionCoords;
        }
        private List<Coords> GetSurroundingCoords(List<Coords> section)
        {
            var surroundingCoords = new List<Coords>();
            foreach(var coord in section)
            {
                for (int i = coord.CoordX - 1; i < coord.CoordX; i++)
                {
                    if (i < 1) i++;
                    if (i > 10) break;
                    for (int j = coord.CoordY - 1; j < coord.CoordY; j++)
                    {
                        if (j < 1) j++;
                        if (j > 10) break;
                        var surCoord = new Coords(i, j);
                        if(!surroundingCoords.Contains(surCoord))
                        {
                            surroundingCoords.Add(surCoord);
                        }
                    }
                }
            }
            return surroundingCoords;
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
