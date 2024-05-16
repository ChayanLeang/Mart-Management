using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;

namespace SayuJapnShop.Controllers
{
    public class ImportController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public ImportController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }

        [HttpGet]
        public IActionResult ImportView(string Date1, string Date2)
        {
            string date1 = "", date2 = "";
            var findImportItem = shopMgContext.VImports.Where(im => string.Compare(im.Date, Date1) >= 0 && string.Compare(im.Date, Date2) <= 0).ToList();

            var import = shopMgContext.VImports.ToList();

            for (int i = 0; i < import.Count; i += import.Count - 1)
            {
                if (i == 0) date1 = import[i].Date;
                if (i == import.Count - 1) date2 = import[i].Date;
            }
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;
            return View(findImportItem);

        }
    }
}
