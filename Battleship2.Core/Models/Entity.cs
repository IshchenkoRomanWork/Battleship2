using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public Entity()
        {
            Id = new Guid();
        }
    }
}
