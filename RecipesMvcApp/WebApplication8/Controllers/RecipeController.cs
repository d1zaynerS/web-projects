using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;

namespace RecipeApp.Controllers
{
    public class RecipeController : Controller
    {
        private static RecipeViewModel _recipe = new RecipeViewModel
        {
            Id = 1,
            Title = "Борщ український",
            Description = "Традиційний український борщ",
            Ingredients = new List<IngredientViewModel>
            {
                new IngredientViewModel { Name = "Буряк", Quantity = 2, Unit = "шт" },
                new IngredientViewModel { Name = "Картопля", Quantity = 4, Unit = "шт" }
            }
        };

        [HttpGet]
        public IActionResult Edit()
        {
            return View(_recipe);
        }

        [HttpPost]
        public IActionResult Edit(RecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _recipe = model;
            ViewBag.Success = "Рецепт успішно збережено!";
            return View(_recipe);
        }
    }
}