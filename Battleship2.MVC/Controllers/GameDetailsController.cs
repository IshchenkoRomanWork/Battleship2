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
        public GameDetailsController(UnitOfWork unitOfWork, BattleShipIdentityContext dBContext)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
            var player = _unitOfWork.GetPlayer(id);
            var detailsList = _unitOfWork.GetGameDetailsList(player);
            var viewModel = new List<GameDetailsViewModel>();
            foreach(var gameDetail in detailsList)
            {
                viewModel.Add(ViewModelParse(gameDetail, player.Id));
            }
            return View(viewModel);
        }
        public IActionResult Details(int Id, int playerId)
        {
            var details = _unitOfWork.GetGameDetails(Id);
            var viewModel = ViewModelParse(details, playerId);
            return View(viewModel);
        }

        private GameDetailsViewModel ViewModelParse(GameDetails details, int playerId)
        {
            var plindex = details.PlayersData.FirstPlayerId == playerId ? 0 : 1;
            var opindex = plindex == 0 ? 1 : 0;
            var shotInfoList = new List<string>();
            var maps = new List<List<List<ViewCell>>>()
            {
                new List<List<ViewCell>>(),
                new List<List<ViewCell>>()
            };
            foreach (var shot in details.ShotList)
            {
                string shotInfo = "Player " + shot.ShooterName +
                    " Shooted at ("
                    + shot.TargetCoords.CoordX.ToString() + ", "
                    + shot.TargetCoords.CoordY.ToString() + ") Coords.";
                if (shot.TargetHit)
                {
                    shotInfo = string.Concat(shotInfo, " Target is hit!");
                }
                if (shot.ShipIsDrown)
                {
                    shotInfo = string.Concat(shotInfo, " Ship is drown!");
                }
                shotInfoList.Add(shotInfo);
            }
            for (int i = 1; i <= 10; i++)
            {
                maps[plindex].Add(new List<ViewCell>());
                maps[opindex].Add(new List<ViewCell>());
                for (int j = 1; j <= 10; j++)
                {
                    maps[plindex][i - 1].Add(new ViewCell()
                    {
                        Ship = details.PlayerMaps[0].GetShipInformation(i, j) == null,
                        Shot = details.PlayerMaps[0].ShotCoords.Any(coord => coord.CoordX == i && coord.CoordY == j)
                    });
                    maps[opindex][i - 1].Add(new ViewCell()
                    {
                        Ship = details.PlayerMaps[1].GetShipInformation(i, j) == null,
                        Shot = details.PlayerMaps[1].ShotCoords.Any(coord => coord.CoordX == i && coord.CoordY == j)
                    });
                }
            }
            return new GameDetailsViewModel()
            {
                PlayerName = 
                new List<string>()
                {
                    details.PlayersData.FirstPlayerName,
                    details.PlayersData.SecondPlayerName
                },
                PlayerMaps = maps,
                ShotInfoList = shotInfoList,
                Id = details.Id,
                PlayerId = playerId
            };
        }
    }
}