using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.MVC.Controllers
{
    public class StatisticsController : Controller
    {
        private UnitOfWork _unitOfWork;
        public StatisticsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
            if (id != default)
            {
                ViewBag.PlayerName = _unitOfWork.GetPlayer(id).Name;
            }
            else
            {
                ViewBag.PlayerName = "";
            }
            return View();
        }
        public IActionResult ReactStatistics(int id)
        {
            if (id != default)
            {
                ViewBag.PlayerName = _unitOfWork.GetPlayer(id).Name;
            }
            else
            {
                ViewBag.PlayerName = "";
            }
            return View();
        }
    }
}