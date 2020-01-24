using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using Battleship2.MVC.Models;
using Battleship2.MVC.Models.ViewModels;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Battleship2.MVC.Controllers
{
    public class AccountController : Controller
    {
        private BattleShipIdentityContext db;
        private UnitOfWork _unitOfWork;

        public AccountController(BattleShipIdentityContext context , UnitOfWork unitOfWork)
        {
            db = context;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                BattleshipIdentity user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Name, user.Id);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect name or password");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                BattleshipIdentity user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
                if (user == null)
                {
                    Player player = new Player() { Name = model.Name };
                    _unitOfWork.CreatePlayer(player);
                    BattleshipIdentity identity = new BattleshipIdentity { Name = model.Name, Password = model.Password, AssociatedPlayerId = player.Id };
                    db.Users.Add(identity);
                    await db.SaveChangesAsync();

                    await Authenticate(model.Name, identity.Id);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString(), "Id")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}