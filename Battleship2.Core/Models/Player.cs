using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public Map CurrentMap { get; set; }
    }
}
