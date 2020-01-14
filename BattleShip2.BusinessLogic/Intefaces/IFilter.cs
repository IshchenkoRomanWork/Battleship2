using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip2.BusinessLogic.Intefaces
{
    public interface IFilter
    {
        public void ApplyFilter(ref IQueryable<StatisticsItem> statistics);
    }
}
