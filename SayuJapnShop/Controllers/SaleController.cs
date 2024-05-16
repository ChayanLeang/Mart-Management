using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class SaleController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public string SessionKeyName = "Name";

        public SaleController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;

        }

        public IActionResult SaleView()
        {
            int total = 0, totalAmount = 0;
            var custoemrList = shopMgContext.Customers.Where(c=>c.status!= "Inactive").ToList();
            if (HttpContext.Session.GetString(SessionKeyName) == "IsDelete")
            {
                var invoice = shopMgContext.Vinvoices.ToList();
                var invoiceItem = invoice[invoice.Count - 1];
                var exportItem = shopMgContext.Vexports.ToList();
                List<Vexport> exportList = new List<Vexport>();
                if (invoiceItem != null)
                {

                    for (int i = 0; i < exportItem.Count; i++)
                    {
                        if (exportItem[i].InvoiceId == invoiceItem.InvoiceId)
                        {
                            exportList.Add(exportItem[i]);
                            totalAmount += exportItem[i].Qty * exportItem[i].Price;
                        }
                    }
                }
                ViewBag.Invoice = invoiceItem;
                ViewBag.Export = exportList;

            }
            var countInvoice = shopMgContext.Invoices.ToList().Count;
            var VsaleItem = shopMgContext.VSales.ToList();
            var sale = shopMgContext.VSales.ToList();

            for (int i = 0; i < VsaleItem.Count; i++)
            {
                total += VsaleItem[i].Amount;
            }

            ViewBag.Total = total;
            ViewBag.TotalAmount = totalAmount;
            ViewBag.CountInvoice = countInvoice + 1;
            ViewBag.Customer = custoemrList;
            ViewBag.Date = DateTime.Today.ToString("yyyy-MM-dd");
            HttpContext.Session.SetString(SessionKeyName, "Not");
            return View(sale);
        }

        [HttpGet]
        public IActionResult AddSale()
        {
            var item = shopMgContext.Items.Where(i=>i.status!= "Inactive").ToList();
            ViewBag.Item = item;
            return View();
        }

        [HttpPost]
        public IActionResult AddSale(Sale sale)
        {

            var sales = shopMgContext.Sales.ToList();
            for (int i = 0; i < sales.Count; i++)
            {
                if (sales[i].ItemId == sale.ItemId)
                {
                    sales[i].Qty += sale.Qty;
                    shopMgContext.Sales.Update(sales[i]);
                    shopMgContext.SaveChanges();
                    return RedirectToAction("SaleView");
                }
            }
            shopMgContext.Sales.Add(sale);
            shopMgContext.SaveChanges();
            return RedirectToAction("SaleView");
        }

        [HttpGet]
        public IActionResult DeleteSale(int id)
        {
            var currentSale = shopMgContext.Sales.SingleOrDefault(sale => sale.ItemId == id);
            if (currentSale != null)
            {
                shopMgContext.Sales.Remove(currentSale);
                shopMgContext.SaveChanges();
            }

            return RedirectToAction("SaleView");
        }

        [HttpGet]
        public ActionResult AddToInvoiceAndDeleteSale(string name, string date)
        {
            int id = 0;
            var purchase = shopMgContext.Purchases.ToList();
            var sale = shopMgContext.Sales.ToList();
            var vsale = shopMgContext.VSales.ToList();
            var cusomter = shopMgContext.Customers.ToList();
            var countInvoice = shopMgContext.Invoices.ToList().Count;
            countInvoice += 1;
            for(int i = 0; i < cusomter.Count; i++)
            {
                if (cusomter[i].CustomerName == name)
                {
                    id = cusomter[i].CustomerId;
                    break;
                }
            }
            for (int i = 0; i < sale.Count; i++)
            {
                for (int j = 0; j < purchase.Count; j++)
                {
                    if (purchase[j].ItemId == sale[i].ItemId)
                    {
                        purchase[j].Qty -= sale[i].Qty;
                        shopMgContext.Purchases.Update(purchase[j]);
                    }
                }

            }

            for (int i = 0; i < sale.Count; i++)
            {

                //Add only one data to Invoice
                if (i == sale.Count - 1)
                {
                    var invoice = new Invoice()
                    {
                        InvoiceId = countInvoice,
                        CustomerID = id,
                        Date = date
                    };
                    shopMgContext.Invoices.Add(invoice);
                }

                var export = new Export()
                {
                    InvoiceId = countInvoice,
                    ItemID = sale[i].ItemId,
                    Price= vsale[i].Price,
                    Qty = sale[i].Qty,
                    Amount = sale[i].Qty * vsale[i].Price
                };

                shopMgContext.Exports.Add(export);
                shopMgContext.Sales.Remove(sale[i]);
            }
            shopMgContext.SaveChanges();

            HttpContext.Session.SetString(SessionKeyName, "IsDelete");
            return RedirectToAction("SaleView");
        }

        [HttpGet]
        public JsonResult GetItemName()
        {
            var pusList = shopMgContext.Vpurchases.ToList();
            return new JsonResult(pusList);
        }

        [HttpGet]
        public JsonResult GetCustomer()
        {
            var customerList = shopMgContext.Customers.ToList();
            return new JsonResult(customerList);
        }
    }
}
