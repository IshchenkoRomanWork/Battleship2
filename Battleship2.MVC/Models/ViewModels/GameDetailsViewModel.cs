using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public Guid Id { get; set; }
        public List<string> PlayerName { get; set; }
        public List<Map> PlayerMaps { get; set; }
        public List<string> ShotInfoList { get; set; }
    }
}
