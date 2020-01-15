using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC
{
    public class FiltersWithSorting
    {
        public List<IFilter> Filters { get; set; }
        public SortingItem Sorting { get; set; }
    }
}
