using Microsoft.AspNetCore.Mvc;
using PizzaStar.Models;
using System.Diagnostics;

namespace PizzaStar.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
