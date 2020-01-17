using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Services
{
    public class Logger : ILogger
    {
        IRepository<LogInfo> _logReposiroty;
        public Logger(IRepository<LogInfo> logReposiroty)
        {
            _logReposiroty = logReposiroty;
        }
        public void Log(string message)
        {
            LogInfo logInfo = new LogInfo(message, DateTime.Now);
            _logReposiroty.Create(logInfo);
        }
    }
}
