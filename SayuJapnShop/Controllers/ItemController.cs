using Microsoft.AspNetCore.Mvc;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class ItemController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public ItemController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult ItemView()
        {
            var item = shopMgContext.Vitems.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            var categories = shopMgContext.Categories.Where(c => c.status != "Inactive").ToList();
            ViewBag.Category=categories;
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {

            if (ValidationItem(item) != "")
            {
                var category = shopMgContext.Categories.ToList();
                ViewBag.Category = category;
                TempData["ItemName"] = ValidationItem(item);
                return View();
            }
            else
            {
                shopMgContext.Items.Add(item);
                shopMgContext.SaveChanges();
                return RedirectToAction("ItemView");
            }
        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var categories=shopMgContext.Categories.Where(c=>c.status!= "Inactive").ToList();
            var checkItem = shopMgContext.Items.SingleOrDefault(i => i.ItemId == id);
            ViewBag.Category = categories;
            return View(checkItem);
        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            if (ValidationItem(item) != "")
            {
                var category = shopMgContext.Categories.ToList();
                ViewBag.Category = category;
                TempData["ItemName"] = ValidationItem(item);
                return View();
            }
            else
            {

                var currentItem = shopMgContext.Items.Find(item.ItemId);
                if (currentItem != null)
                {
                    currentItem.ItemName = item.ItemName;
                    currentItem.CategoryId = item.CategoryId;
                    currentItem.status = item.status;
                    shopMgContext.Items.Update(currentItem);
                    shopMgContext.SaveChanges();
                }
                return RedirectToAction("ItemView");
            }

        }

        private string ValidationItem(Item item)
        {
            string erorr = "";
            var items = shopMgContext.Items.Where(it => it.ItemId != item.ItemId).ToList();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemName == item.ItemName)
                {
                    erorr = "ItemName is Already exist";
                }
            }
            return erorr;
        }
    }
}