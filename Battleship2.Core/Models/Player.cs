using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    class Player : Entity, IPlayer
    {
        public string Name { get; set; }
    }
}
