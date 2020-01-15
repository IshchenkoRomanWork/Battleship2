using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models
{
    public class BattleshipIdentity
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid AssociatedPlayerId { get; set; }
    }
}
