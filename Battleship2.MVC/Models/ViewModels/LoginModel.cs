using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Name isn't specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Passowrd isn't specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
