using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Models.Filters
{
    public class GameTurnFilter : IFilter
    {
        public int MinimalGameTurns { get; set; }
        public void ApplyFilter(ref IQueryable<StatisticsItem> statistics)
        {
            statistics = statistics.Where(si => si.GameTurnNumber > MinimalGameTurns);
        }
        public GameTurnFilter(int minimalGameTurns)
        {
            MinimalGameTurns = minimalGameTurns;
        }
    }
}
