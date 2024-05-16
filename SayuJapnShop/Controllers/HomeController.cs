using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;
using System.Diagnostics;

namespace SayuJapnShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public HomeController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult HomeView()
        {

            int expense = 0, de = 0;
            //var import = shopMgContext.Vimports.ToList();
            int customerCount = shopMgContext.Customers.ToList().Count;
            int userCount = shopMgContext.Users.ToList().Count;
            int itemCount = shopMgContext.Items.ToList().Count;
            int supplierCount = shopMgContext.Suppliers.ToList().Count;

           

            /*for (int i = 0; i < import.Count; i++)
            {
                expense += import[i].Qty * import[i].Cost;
            }*/

            ViewBag.customerCount = customerCount;
            ViewBag.userCount = userCount;
            ViewBag.itemCount = itemCount;
            ViewBag.supplierCount = supplierCount;
            ViewBag.Debt = de;
            ViewBag.Expense = expense;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}