using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Hubs
{
    public class BattleShipHub : Hub
    {
        private UnitOfWork _unitOfWork { get; set; }
        private ActiveGames _activeGames { get; set; }
        public BattleShipHub(UnitOfWork unitOfWork, ActiveGames activeGames)
        {
            _unitOfWork = unitOfWork;
            _activeGames = activeGames;
        }
        public void PlayerConnected()
        {

        }
        public void AddShip()
        {

        }
        public void Ready()
        {

        }
        public void Shot()
        {

        }
        public void Win()
        {

        }
    }
}
