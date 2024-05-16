using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SayuJapnShop.DataDb;
using SayuJapnShop.Models;

namespace SayuJapnShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShDbContext shopMgContext;
        public CategoryController(ShDbContext shopMgContext)
        {
            this.shopMgContext = shopMgContext;
        }
        public IActionResult CategoryView()
        {
            var category = shopMgContext.Categories.ToList();
            return View(category);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {

            if (!ValidationCategory(category).IsNullOrEmpty())
            {
                ViewBag.ErrorList = ValidationCategory(category);
                return View();
            }
            else
            {
                shopMgContext.Categories.Add(category);
                shopMgContext.SaveChanges();
                return RedirectToAction("CategoryView");
            }
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var checkCategory = shopMgContext.Categories.SingleOrDefault(x => x.CategoryId == id);
            return View(checkCategory);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (!ValidationCategory(category).IsNullOrEmpty())
            {
                ViewBag.ErrorList = ValidationCategory(category);
                return View();
            }
            else
            {
                var currentCategory = shopMgContext.Categories.Find(category.CategoryId);
                if (currentCategory != null)
                {
                    currentCategory.CategoryName = category.CategoryName;
                    currentCategory.Descriptions = category.Descriptions;
                    currentCategory.status = category.status;
                    shopMgContext.Categories.Update(currentCategory);
                    shopMgContext.SaveChanges();
                }
                return RedirectToAction("CategoryView");
            }

        }

        private List<string> ValidationCategory(Category category)
        {
            List<string> errorList = new List<string>();
            var categories = shopMgContext.Categories.Where(c => c.CategoryId != category.CategoryId).ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].CategoryName == category.CategoryName)
                {
                    errorList.Add("CategoryName Already Exist");
                }

                if (categories[i].Descriptions == category.Descriptions)
                {
                    errorList.Add("Descriptions Already Exist");
                }
            }
            return errorList;
        }
    }
}
