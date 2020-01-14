using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.MVC.Controllers
{
    public class GameController : Controller
    {
        //private UnitOfWork _unitOfWork {get; set;}
        //public GameController(UnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConnectToGame(Guid playerId)
        {
            return View();
        }
        public IActionResult CreateGame()
        {
            return View();
        }
        public IActionResult Win()
        {
            return View();
        }
        public IActionResult CloseGame()
        {
            return View();
        }
    }
}