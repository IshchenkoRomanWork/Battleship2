using BattleShip2.BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Models
{
    public class SortingItem
    {
        public SortingType SortingType { get; set; }
        public SortingDirection SortingDirection {get; set;}

        public SortingItem(SortingType type, SortingDirection direction)
        {
            SortingType = type;
            SortingDirection = direction;
        }
    }
}
