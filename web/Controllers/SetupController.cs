using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web.Models;
using web.Classes;

namespace web.Controllers
{
    public class SetupController : Controller
    {
        private readonly ILogger<SetupController> _logger;
        private readonly ChatContext _context;

        public SetupController(ILogger<SetupController> logger, ChatContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var existingAdmin = _context.Users.FirstOrDefault(m => m.RoleEnum == 0);
            if (existingAdmin != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(SetupModel model)
        {
            var existingAdmin = _context.Users.FirstOrDefault(m => m.RoleEnum == 0);

            if (existingAdmin != null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var setupSecret = "secretS"; //TODO: Get from appsettings
                if (string.Compare(setupSecret, model.SetupSecret) != 0)
                {
                    ModelState.AddModelError("SetupSecret", "Invalid Setup Secret");
                }

                var existingUser = _context.Users.FirstOrDefault(m => m.UserName == model.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "User with this name already exists.");
                }

                if (string.Compare(model.Password, model.ConfirmPassword, false) != 0)
                {
                    ModelState.AddModelError("Password", "Passwords do not match!");
                }

                if (ModelState.IsValid)
                {
                    User newUser = new User();
                    newUser.UserName = model.UserName;
                    newUser.Salt = PasswordHelper.GenerateSalt();
                    newUser.Password = PasswordHelper.GenerateSaltedHash(model.Password, newUser.Salt);
                    newUser.RoleEnum = 0; //Admin
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
