using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
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
