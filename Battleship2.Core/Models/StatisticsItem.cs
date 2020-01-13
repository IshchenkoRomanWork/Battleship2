using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class StatisticsItem : Entity
    {
        public int GameTurnNumber { get; set; }
        public string WinnerName { get; set; }
        public ICollection<Ship> RemainingShips { get; set; }
    }
}
