using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.MVC.Controllers
{
    public class GameDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GameDetails(Guid gameDetailsId)
        {
            return View();
        }
    }
}