using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IPlayer : IEntity
    {
        public string Name { get; set; }
    }
}
