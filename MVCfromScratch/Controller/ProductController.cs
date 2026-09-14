using Microsoft.AspNetCore.Mvc;
using MVCfromScratch.Models;
using MVCfromScratch.ViewModels;

namespace MVCfromScratch.Controllers
{
    public class ProductsController : Controller
    {

        public IActionResult Index()
        {
            var products = ProductsRepository.GetProducts(loadCategory: true);
            return View(products);
        }

        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel();
            {
                productViewModel.Categories = CategoriesRepository.GetCategories();
            }
            ViewBag.Action = "Add";
            return View(productViewModel);
        }
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }

            productViewModel.Categories = CategoriesRepository.GetCategories();
            ViewBag.Action = "Edit";
            return View(productViewModel);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ProductsRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}