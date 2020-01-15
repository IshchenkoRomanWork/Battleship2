using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Battleship2.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Battleship2.MVC.Models.ViewModels;
using Battleship2.Core.Models;

namespace Battleship2.MVC.Controllers
{
    [Authorize]
    public class GameDetailsController : Controller
    {
        private UnitOfWork _unitOfWork;
        private BattleshipIdentityContext _dBContext;
        public GameDetailsController(UnitOfWork unitOfWork, BattleshipIdentityContext dBContext)
        {
            _unitOfWork = unitOfWork;
            _dBContext = dBContext;
        }
        public IActionResult Index()
        {
            var IdentityId = Request.HttpContext.User.Claims.First(c => c.ValueType == "Id").Value;
            var Identity = _dBContext.Set<BattleshipIdentity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == Guid.Parse(IdentityId)).Result;
            var player = _unitOfWork.GetPlayer(Identity.AssociatedPlayerId);
            var detailsList = _unitOfWork.GetGameDetailsList(player);
            var viewModel = new List<GameDetailsViewModel>();
            foreach(var gameDetail in detailsList)
            {
                viewModel.Add(ViewModelParse(gameDetail));
            }
            return View(viewModel);
        }
        public IActionResult GameDetailsItem(Guid gameDetailsId)
        {
            var details = _unitOfWork.GetGameDetails(gameDetailsId);
            var viewModel = ViewModelParse(details);
            return View(viewModel);
        }

        private GameDetailsViewModel ViewModelParse(GameDetails details)
        {
            var shotInfoList = new List<string>();
            foreach (var shot in details.ShotList)
            {
                string shotInfo = "Player " + shot.Shooter.Name +
                    " Shooted at ("
                    + shot.TargetCoords.CoordX.ToString() + ", "
                    + shot.TargetCoords.CoordY.ToString() + ") Coords.";
                if (shot.TargetHit)
                {
                    string.Concat(shotInfo, " Target is hit!");
                }
                if (shot.ShipIsDrown)
                {
                    string.Concat(shotInfo, " Ship is drown!");
                }
            }
            return new GameDetailsViewModel()
            {
                PlayerName = details.Players.Select(p => p.Name).ToList(),
                PlayerMaps = details.PlayerMaps.ToList(),
                ShotInfoList = shotInfoList,
                Id = details.Id
            };
        }
    }
}