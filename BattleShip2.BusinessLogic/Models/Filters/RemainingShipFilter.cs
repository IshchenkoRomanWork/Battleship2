using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Models.Filters
{
    class RemainingShipFilter
    {
        public int MinimalRemaininShipNumber { get; set; }
        public void ApplyFilter(ref IQueryable<StatisticsItem> statistics)
        {
            statistics = statistics.Where(si => si.RemainingShips.Count > MinimalRemaininShipNumber);
        }
    }
}
