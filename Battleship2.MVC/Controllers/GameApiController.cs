using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using Battleship2.MVC.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.MVC.Controllers
{
    [Route("api/gameapi")]
    [ApiController]
    public class GameApiController : ControllerBase
    {
        private Helper _helper { get; set; }
        public GameApiController(Helper helper)
        {
            _helper = helper;
        }
        [HttpGet("getplayerid", Name = "GetPlayerId")]
        public string GetPlayerId(string data)
        {
            Player player =_helper.GetPlayerFromRequest(HttpContext.Request);
            return player.Id.ToString();
        }
        [HttpGet("islogged", Name = "IsLoggedIn")]
        public bool IsLogged()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }
}