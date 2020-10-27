using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web.Models;
using web.Classes;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChatContext _context;

        public HomeController(ILogger<HomeController> logger, ChatContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var existingAdmin = _context.Users.FirstOrDefault(m => m.RoleEnum == 0);

            if (existingAdmin == null)
            {
                return RedirectToAction("Index", "Setup");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(m => m.UserName == model.UserName);

                if (existingUser == null)
                {
                    ModelState.AddModelError("UsernNme", "Invalid Username or Password.");
                }
                else
                {
                    var hashedPassword = PasswordHelper.GenerateSaltedHash(model.Password, existingUser.Salt);
                    if (!PasswordHelper.CompareByteArrays(hashedPassword, existingUser.Password))
                    {
                        ModelState.AddModelError("UserName", "Invalid Username or Password");
                    }
                }

                if (ModelState.IsValid)
                {
                    var role = existingUser.RoleEnum == 0 ? "Administrator" : "User";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Role, role),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Chat");
                }


            }
            return View(model);
        }

        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
