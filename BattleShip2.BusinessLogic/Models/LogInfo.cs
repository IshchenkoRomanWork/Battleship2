using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Models
{
    public class LogInfo : Entity
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public LogInfo(string message, DateTime dateTime) : base()
        {
            Message = message;
            DateTime = dateTime;
        }
    }
}
