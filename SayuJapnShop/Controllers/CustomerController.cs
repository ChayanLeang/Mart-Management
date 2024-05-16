using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public CustomerController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult CustomerView()
        {
            var cusomter = shopMgContext.Customers.ToList();
            return View(cusomter);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {

            if (ValidationCustomer(customer) != "")
            {
                TempData["PhoneNumber"] = ValidationCustomer(customer);
                return View();
            }
            else
            {
                shopMgContext.Customers.Add(customer);
                shopMgContext.SaveChanges();
                return RedirectToAction("CustomerView");
            }

        }

        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            var checkCustomer = shopMgContext.Customers.SingleOrDefault(c => c.CustomerId == id);
            return View(checkCustomer);
        }

        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            if (ValidationCustomer(customer) != "")
            {
                TempData["PhoneNumber"] = ValidationCustomer(customer);
                return View();
            }
            else
            {
                var currentCustomer = shopMgContext.Customers.Find(customer.CustomerId);
                if (currentCustomer != null)
                {
                    currentCustomer.CustomerName = customer.CustomerName;
                    currentCustomer.Gender = customer.Gender;
                    currentCustomer.PhoneNumber = customer.PhoneNumber;
                    currentCustomer.Address = customer.Address;
                    currentCustomer.status = customer.status;
                    shopMgContext.Customers.Update(currentCustomer);
                    shopMgContext.SaveChanges();
                }
                return RedirectToAction("CustomerView");
            }

        }

        private string ValidationCustomer(Customer customer)
        {
            string error = "";
            var customers = shopMgContext.Customers.Where(c => c.CustomerId != customer.CustomerId).ToList();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].PhoneNumber == customer.PhoneNumber)
                {
                    error = "PhoneNumber is already exist";
                }
            }
            return error;
        }
    }
}
