using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Models.Filters
{
    public class PlayerFilter : IFilter
    {
        public string PlayerName { get; set; }
        public void ApplyFilter(ref IQueryable<StatisticsItem> statistics)
        {
            statistics = statistics.Where(si => si.WinnerName == PlayerName);
        }
        public PlayerFilter(string name)
        {
            PlayerName = name;
        }
    }
}
