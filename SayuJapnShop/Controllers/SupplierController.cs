using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public SupplierController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult SupplierView()
        {
            var supplier = shopMgContext.Suppliers.ToList();
            return View(supplier);
        }

        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            if (!ValidationSupplier(supplier).IsNullOrEmpty())
            {
                ViewBag.ErrorList = ValidationSupplier(supplier);
                return View();
            }
            else
            {
                shopMgContext.Suppliers.Add(supplier);
                shopMgContext.SaveChanges();
                return RedirectToAction("SupplierView");
            }

        }

        [HttpGet]
        public IActionResult EditSupplier(int id)
        {
            var checkSupplier = shopMgContext.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            return View(checkSupplier);
        }

        [HttpPost]
        public IActionResult EditSupplier(Supplier supplier)
        {
            if (!ValidationSupplier(supplier).IsNullOrEmpty())
            {
                ViewBag.ErrorList = ValidationSupplier(supplier);
                return View();
            }
            else
            {
                var currentSupplier = shopMgContext.Suppliers.Find(supplier.SupplierId);
                if (currentSupplier != null)
                {
                    currentSupplier.CompanyName = supplier.CompanyName;
                    currentSupplier.Owner = supplier.Owner;
                    currentSupplier.status = supplier.status;
                    currentSupplier.PhoneNumber = supplier.PhoneNumber;
                    currentSupplier.Address = supplier.Address;
                    shopMgContext.Suppliers.Update(currentSupplier);
                    shopMgContext.SaveChanges();
                }
                return RedirectToAction("SupplierView");
            }

        }

        private List<string> ValidationSupplier(Supplier supplier)
        {
            List<string> errorList = new List<string>();
            var suppliers = shopMgContext.Suppliers.Where(s => s.SupplierId != supplier.SupplierId).ToList();
            for (int i = 0; i < suppliers.Count; i++)
            {
                if (suppliers[i].PhoneNumber == supplier.PhoneNumber)
                {
                    errorList.Add("CompanyName is Already exist");
                }

                if (suppliers[i].CompanyName == supplier.CompanyName)
                {
                    errorList.Add("PhoneNumber is Already exist");
                }
            }
            return errorList;
        }
    }
}
