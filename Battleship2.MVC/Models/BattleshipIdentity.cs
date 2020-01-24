using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.MVC
{
    public class BattleshipIdentity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int AssociatedPlayerId { get; set; }
    }
}
