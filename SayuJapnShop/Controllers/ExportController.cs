using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class ExportController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public ExportController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult ExportView(string Date1, string Date2)
        {
            int m;
            string date1 = "", date2 = "";
            var invoice = shopMgContext.Vinvoices.Where(inv => string.Compare(inv.Date, Date1) >= 0 && string.Compare(inv.Date, Date2) <= 0).ToList();
            var exportItem = shopMgContext.Vexports.ToList();

            List<Vexport> storeInvoiceItems = new List<Vexport>();
            List<Vexport> firstInvoiceItems = new List<Vexport>();

            for (int i = 0; i < exportItem.Count; i += exportItem.Count - 1)
            {
                if (i == 0) date1 = exportItem[i].Date;
                else if (i == exportItem.Count - 1) date2 = exportItem[i].Date;
            }

            for (int i = 0; i < invoice.Count; i++)
            {
                m = 0;
                for (int j = 0; j < exportItem.Count; j++)
                {
                    if (exportItem[j].InvoiceId == invoice[i].InvoiceId)
                    {
                        m++;
                        if (m == 1)
                        {
                            firstInvoiceItems.Add(exportItem[j]);
                        }
                        else
                        {
                            storeInvoiceItems.Add(exportItem[j]);
                        }
                    }

                }
            }

            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;
            ViewBag.FirstInvoice = firstInvoiceItems;
            ViewBag.InvoiceItems = storeInvoiceItems;
            return View(invoice);
        }
    }
}
