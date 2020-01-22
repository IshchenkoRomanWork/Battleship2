using Battleship2.Core.Enums;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class Ship : Entity
    {
        public ShipType Type { get; set; }
        public List<DeckState> DeckStates { get; set; }

        public Ship(int deckNumber)
        {
            Type = (ShipType)deckNumber;
            DeckStates = new List<DeckState>();
            for(int i = 0; i < deckNumber; i++)
            {
                DeckStates.Add(DeckState.Undamaged);
            }
        }
        public Ship()
        {
           
        }
    }
}
