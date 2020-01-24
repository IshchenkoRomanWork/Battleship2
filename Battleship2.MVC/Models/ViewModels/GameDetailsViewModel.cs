using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<string> PlayerName { get; set; }
        public List<List<List<ViewCell>>> PlayerMaps { get; set; }
        public List<string> ShotInfoList { get; set; }
    }
}
