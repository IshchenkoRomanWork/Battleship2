﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Battleship2.MVC.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index(Guid? playerid)
        {
            return View();
        }
    }
}