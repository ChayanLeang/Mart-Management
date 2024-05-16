using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;
using System.Security.Claims;

namespace SayuJapnShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public AuthController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogIn user)
        {
            if (ModelState.IsValid)
            {
                var checkUser = shopMgContext.Users.SingleOrDefault(u => u.UserName == user.UserName);
                if (checkUser != null)
                {
                    bool isValid = checkUser.UserName == user.UserName && checkUser.Password == user.Password;
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("UserName", user.UserName);
                        return RedirectToAction("HomeView", "Home");
                    }
                    else
                    {
                        TempData["ErrorPassword"] = "Wrong Password";
                        return View();
                    }
                }
                else
                {
                    TempData["ErrorEmail"] = "Wrong UserName";
                    return View();
                }

            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookie in storedCookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("LogIn", "Auth");
        }
    }
}
