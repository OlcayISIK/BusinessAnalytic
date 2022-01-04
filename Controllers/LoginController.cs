using BusinessAnalytic.Models.Concrete.EfCore;
using BusinessAnalytic.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessAnalytic.Controllers
{
    public class LoginController : Controller
    {
        AdventureWorks2019Context context = new AdventureWorks2019Context();
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(User user)
        {
            var informations = context.Users.FirstOrDefault(x => x.Name == user.Name && x.Password == user.Password);
            if(informations != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                };
                var userIdentity = new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal); 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Name,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (UserExists(user.Name))
                {
                    context.Add(user);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Register));
                }
                else
                {
                    ViewBag.UserExist = "User is Already Exist";
                }

            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        private bool UserExists(string name)
        {
            return context.Users.Any(e => e.Name == name);
        }





    }
}
