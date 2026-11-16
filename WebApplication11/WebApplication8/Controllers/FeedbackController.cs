using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;

namespace RecipeApp.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FeedbackViewModel());
        }

        [HttpPost]
        public IActionResult Index(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {

                model.IsSent = true;
            }
            return View(model);
        }
    }
}