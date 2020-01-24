using BattleShip2.BusinessLogic.Intefaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        ILogger _logger;
        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            _logger.Log(filterContext.Exception.Message + " / " + filterContext.Exception.InnerException + " / " + filterContext.Exception.StackTrace);
        }
    }
}
