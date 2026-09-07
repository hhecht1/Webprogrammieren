
using Microsoft.AspNetCore.Mvc;
using MVCfromScratch;
using MVCfromScratch.Models;

namespace MVCfromScratch.Controllers
{
    public class CategoriesController : Controller
    {
        // public IActionResult IndexTest()
        // {
        //     return View();
        // }

        // public IActionResult Edit(int id)  // auch mal (string id) testen
        // {
        //     return new ContentResult { Content = id.ToString() };
        // }

        // public IActionResult Edit(int? id)
        // {
        //     if (id.HasValue)
        //         return new ContentResult { Content = id.ToString() };
        //     else
        //         return new ContentResult { Content = "no result" };
        // }
        // public IActionResult Edit(int? id)
        // {
        //     var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };

        //     return View(category);   // Objekt wird an die View übergeben
        // }
        public IActionResult Index()
        {
            var categories = CategoriesRepository.GetCategories();
            return View(categories);
        }


        public IActionResult Edit(int? id)      // [FromHeader]int? id
        {
            // var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };

            if (id == null)
                return NotFound();

            var category = CategoriesRepository.GetCategoryById(id.Value);
            if (category == null)
                return NotFound();

            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoriesRepository.UpdateCategory(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);


        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            CategoriesRepository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }

    }
}