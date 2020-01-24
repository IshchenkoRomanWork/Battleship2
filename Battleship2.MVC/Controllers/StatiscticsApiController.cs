using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Enums;
using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Models.Filters;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Battleship2.MVC.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : Controller
    {
        private UnitOfWork _unitOfWork { get; set; }
        public StatisticsApiController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("get", Name = "GetStatistics")]
        public StatisticsItem[] GetStatistics(string data)
        {
            JObject json = JObject.Parse(data);
            FiltersWithSorting filtersWithSorting = GetFiltersWithSortingFromJson(json);
            var statisticList = _unitOfWork.GetStatistics(filtersWithSorting.Filters, filtersWithSorting.Sorting);
            return statisticList.ToArray();
        }
        private FiltersWithSorting GetFiltersWithSortingFromJson(JObject json)
        {
            var sorting = json["sorting"].ToString();
            var name = json["name"].ToString();
            var gameDates = json["gameDates"].ToString();
            var remainingShips = json["remainingShips"].ToString() == "" ? -1 : json["remainingShips"].ToObject<int>();
            var gameTurns = json["gameTurns"].ToString() == "" ? -1 : json["gameTurns"].ToObject<int>();

            var sortingItemElements = sorting.Split('/');
            var datesItems = gameDates.Split(" - ");

            SortingDirection sortingDirection = sortingItemElements[0] switch
            {
                "desc" => SortingDirection.Descending,
                "asc" => SortingDirection.Ascending
            };
            SortingType sortingType = sortingItemElements[1] switch
            {
                "ship" => SortingType.RemainingShipsSorting,
                "date" => SortingType.DateSorting
            };
            DateTime firstDate;
            DateTime secondDate;
            List<IFilter> filters = new List<IFilter>();
            if (DateTime.TryParse(datesItems[0], out firstDate) && DateTime.TryParse(datesItems[1], out secondDate))
            {
                filters.Add(new DateFilter(firstDate, secondDate));
            }
            if (!string.IsNullOrEmpty(name))
            {
                filters.Add(new PlayerFilter(name));
            }
            if (remainingShips != -1)
            {
                filters.Add(new RemainingShipFilter(remainingShips));
            }
            if (gameTurns != -1)
            {
                filters.Add(new GameTurnFilter(gameTurns));
            }

            return new FiltersWithSorting()
            {
                Filters = filters,
                Sorting = new SortingItem(sortingType, sortingDirection)
            };
        }
    }
}