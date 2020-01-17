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
        [NonAction]
        public IActionResult Game(GameViewModel viewModel, bool isCreated)
        {
            var player = _helper.GetPlayerFromRequest(HttpContext.Request);
            ViewBag.IsCreated = isCreated;
            if (isCreated)
            {
                ViewBag.PlayerId = player.Id.ToString();
            }
            return View(viewModel);
        }
        public IActionResult Game(string gameId)
        {
            if(string.IsNullOrEmpty(gameId))
            {
                return Game(new GameViewModel() { GameId = null }, true);
            }
            GameViewModel viewModel = new GameViewModel() { GameId = gameId };
            return Game(viewModel, false);
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