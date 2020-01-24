using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class PlayersData : Entity
    {
        public int FirstPlayerId { get; set; }
        public int SecondPlayerId { get; set; }
        public string FirstPlayerName { get; set; }
        public string SecondPlayerName { get; set; }
    }
}
