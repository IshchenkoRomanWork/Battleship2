using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Models.Filters
{
    public class DateFilter : IFilter
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }

        public void ApplyFilter(ref IQueryable<StatisticsItem> statistics)
        {
            statistics = statistics.Where(si => si.GameDate > StartingDate && si.GameDate < EndDate);
        }
        public DateFilter(DateTime start, DateTime end)
        {
            StartingDate = start;
            EndDate = end;
        }
    }
}
