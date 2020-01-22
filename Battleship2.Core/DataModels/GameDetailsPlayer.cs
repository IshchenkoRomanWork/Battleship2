using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.DataModels
{
    public class GameDetailsPlayer : Entity
    {
        public GameDetails GameDetails { get; set; }
        public Guid GameDetailsId { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
    }
}
