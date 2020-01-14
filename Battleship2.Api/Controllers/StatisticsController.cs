using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.Api.Controllers
{
    public class StatisticsController : Controller
    {
        //private UnitOfWork _unitOfWork {get; set;}
        //public StatisticsController(UnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public IActionResult GetStatistics()
        {
            return View();
        }
    }
}