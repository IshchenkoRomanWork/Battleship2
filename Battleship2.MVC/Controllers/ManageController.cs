using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.MVC.Models;
using Battleship2.MVC.Models.ViewModels;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Battleship2.MVC.Controllers
{
    public class ManageController : Controller
    {
        private Helper _helper;
        public ManageController(Helper helper)
        {
            _helper = helper;
        }
        public IActionResult Index()
        {
            var player = _helper.GetPlayerFromRequest(HttpContext.Request);
            return View(new PlayerViewModel() { Id = player.Id, Name = player.Name });
        }
    }
}