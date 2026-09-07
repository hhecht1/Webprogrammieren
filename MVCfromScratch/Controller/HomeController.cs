using Microsoft.AspNetCore.Mvc;

namespace MVCfromScratch.Controllers
{
    public class HomeController : Controller    // abgeleitet von ControllerBase (für APIs ohne View-Funktionalität)
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "First View Title";
            return View();
        }

        //public string Index()
        //{
        //    return "Hi vom HomeController";       // Rückgabe von string möglich
        //}

        public string Error()
        {
            return "Hi vom HomeController. Fehler!";
        }
    }
}