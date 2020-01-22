using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core
{
        public class BattleshipIdentity
        {
            public Guid Id { get; private set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public Guid AssociatedPlayerId { get; set; }
        }
}
