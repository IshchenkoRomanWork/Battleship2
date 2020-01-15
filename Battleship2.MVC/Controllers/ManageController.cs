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
        private UnitOfWork _unitOfWork;
        private BattleshipIdentityContext _dBContext;
        public ManageController(UnitOfWork unitOfWork, BattleshipIdentityContext dBContext)
        {
            _unitOfWork = unitOfWork;
            _dBContext = dBContext;
        }
        public IActionResult Index()
        {
            var IdentityId = Request.HttpContext.User.Claims.First(c => c.ValueType == "Id").Value;
            var Identity = _dBContext.Set<BattleshipIdentity>().AsQueryable().FirstOrDefaultAsync(e => e.Id == Guid.Parse(IdentityId)).Result;
            var player = _unitOfWork.GetPlayer(Identity.AssociatedPlayerId);
            return View(new PlayerViewModel() { Id = player.Id, Name = player.Name });
        }
    }
}