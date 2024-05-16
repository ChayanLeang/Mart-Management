using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class UserController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public UserController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult StoreUserView()
        {
            var userName = User.Identity.Name;
            var user = shopMgContext.Users.SingleOrDefault(user => user.UserName == userName);
            return View(user);
        }
        public IActionResult UserView()
        {
            var user = shopMgContext.Users.ToList();
            return View(user);
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var checkUser = shopMgContext.Users.SingleOrDefault(u => u.Email == user.Email || u.Password == user.Password);
                if (checkUser != null)
                {
                    bool IsExistPassword = checkUser.Password == user.Password;
                    bool IsExistEmail = checkUser.Email == user.Email;
                    if (IsExistPassword)
                    {
                        TempData["ErrorPassword"] = "Password is Exist";
                    }
                    if (IsExistEmail)
                    {
                        TempData["ErrorEmail"] = "Email is Exist";
                    }
                    return View();
                }
                else
                {
                    shopMgContext.Users.Add(user);
                    shopMgContext.SaveChanges();
                    return RedirectToAction("UserView");
                }

            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var currentUser = shopMgContext.Users.SingleOrDefault(user => user.UserId == id);
            return View(currentUser);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            var currentUser = shopMgContext.Users.Find(user.UserId);
            if (currentUser != null)
            {
                currentUser.UserName = user.UserName;
                currentUser.Password = user.Password;
                currentUser.Email = user.Email;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Role = user.Role;
                shopMgContext.Users.Update(currentUser);
                shopMgContext.SaveChanges();
            }
            return RedirectToAction("UserView");
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var currentUser = shopMgContext.Users.SingleOrDefault(user => user.UserId == id);
            return View(currentUser);
        }

        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            var currentUser = shopMgContext.Users.Find(user.UserId);
            if (currentUser != null)
            {
                shopMgContext.Users.Remove(currentUser);
                shopMgContext.SaveChanges();
            }
            return RedirectToAction("UserView");
        }
    }
}
