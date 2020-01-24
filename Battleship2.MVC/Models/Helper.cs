using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models
{
    public class Helper
    {
        private UnitOfWork _unitOfWork;
        private BattleShipIdentityContext _dBContext;
        public Helper(UnitOfWork unitOfWork, BattleShipIdentityContext dBContext)
        {
            _unitOfWork = unitOfWork;
            _dBContext = dBContext;
        }

        public Player GetPlayerFromRequest(HttpRequest request)
        {
            var IdentityId = request.HttpContext.User.Claims.First(c => c.ValueType == "Id").Value;
            var Identity = _dBContext.Set<BattleshipIdentity>().AsQueryable().FirstOrDefaultAsync(e => e.Id == Int32.Parse(IdentityId)).Result;
            return _unitOfWork.GetPlayer(Identity.AssociatedPlayerId);
        }
    }
}
