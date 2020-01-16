using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models.ViewModels
{
    public class GameViewModel
    {
        public string Guid { get; set; }
        public List<Map> YourAndOpponentsMaps { get; set; }
    }
}
