using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using Battleship2.MVC;
using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Battleship2.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : Controller
    {
        private UnitOfWork _unitOfWork { get; set; }
        public StatisticsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("[action]")]
        public List<StatisticsItem> Get(string requestQuery)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            FiltersWithSorting item = JsonConvert.DeserializeObject<FiltersWithSorting>(requestQuery, settings);
            return _unitOfWork.GetStatistics(item.Filters, item.Sorting);
        }
    }
}