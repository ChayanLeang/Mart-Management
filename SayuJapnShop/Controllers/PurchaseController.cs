using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public PurchaseController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult PurchaseView()
        {
            var purchase = shopMgContext.Vpurchases.ToList();
            return View(purchase);
        }

        [HttpGet]
        public IActionResult AddPurchase()
        {

            var item = shopMgContext.Items.Where(i => i.status != "Inactive").ToList();
            var supplier = shopMgContext.Suppliers.Where(s => s.status != "Inactive").ToList();
            ViewBag.Supplier = supplier;
            ViewBag.Item = item;
            ViewBag.Date = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public IActionResult AddPurchase(MainPurchase purchase)
        {

            var oldPurchase = shopMgContext.Purchases.ToList();
            int k = oldPurchase.Count;

            for (int i = 0; i < oldPurchase.Count; i++)
            {
                if (oldPurchase[i].ItemId == purchase.ItemId)
                {
                    k = i;
                    var import = new Import()
                    {
                        ItemID = purchase.ItemId,
                        SupplierID = purchase.SupplierId,
                        Cost = purchase.Cost,
                        Price = purchase.Price,
                        Date = purchase.Date,
                        Remains = oldPurchase[i].Qty,
                        Qty= purchase.Qty,
                    };
                    oldPurchase[i].Qty += purchase.Qty;
                    oldPurchase[i].Price = purchase.Price;
                    oldPurchase[i].Cost = purchase.Cost;
                    oldPurchase[i].Date = purchase.Date;

                    shopMgContext.Purchases.Update(oldPurchase[i]);
                    shopMgContext.Imports.Add(import);
                    break;
                }
            }
            if (k == oldPurchase.Count)
            {
                var import = new Import()
                {
                    ItemID = purchase.ItemId,
                    SupplierID = purchase.SupplierId,
                    Remains = 0,
                    Price= purchase.Price,
                    Date = purchase.Date,
                    Cost = purchase.Cost,
                    Qty = purchase.Qty,
                };
                var pur = new Purchase()
                {
                    ItemId = purchase.ItemId,
                    Cost = purchase.Cost,
                    Price = purchase.Price,
                    Qty = purchase.Qty,
                    Date = purchase.Date,

                };
                shopMgContext.Purchases.Add(pur);
                shopMgContext.Imports.Add(import);
            }

            shopMgContext.SaveChanges();
            return RedirectToAction("PurchaseView");
        }
    }
}
