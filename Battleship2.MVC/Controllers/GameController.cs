using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.MVC.Hubs;
using Battleship2.MVC.Models;
using Battleship2.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Battleship2.MVC.Controllers
{
    public class GameController : Controller
    {
        private Helper _helper;
        public GameController(Helper helper)
        {
            _helper = helper;

        }
        public IActionResult Game(string gameid, bool isCreated)
        {
            var player = _helper.GetPlayerFromRequest(HttpContext.Request);
            ViewBag.IsCreated = isCreated;
            ViewBag.PlayerId = player.Id.ToString();
            return View(new GameViewModel() { GameId = gameid });
        }
        public IActionResult Win()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CloseGame()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}