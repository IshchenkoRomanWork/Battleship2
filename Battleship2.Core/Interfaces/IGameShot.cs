using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IGameShot : IEntity
    {
        public (int, int) TargetCoords { get; set; }
        public Guid ShooterId { get; set; }
        public bool TargetHit { get; set; }
        public bool ShipIsDrown { get; set; }
    }
}
