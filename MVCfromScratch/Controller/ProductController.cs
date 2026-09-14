using Microsoft.AspNetCore.Mvc;
using MVCfromScratch.Models;

namespace MVCfromScratch.Controllers
{
    public class ProductsController : Controller
    {

        public IActionResult Index()
        {
            var products = ProductsRepository.GetProducts();
            return View(products);
        }
    }
}