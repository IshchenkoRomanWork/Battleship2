using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    interface IGameDetails : IEntity
    {
        public ICollection<Guid> Players { get; set; }
        public ICollection<IMap> PlayerMaps { get; set; }
        public ICollection<IGameShot> ShotList { get; set; }
        public ICollection<IShip> WinnerShips { get; set; }
        public Guid Winner { get; set; }
    }
}
