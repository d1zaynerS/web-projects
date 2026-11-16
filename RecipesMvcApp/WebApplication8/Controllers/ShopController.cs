using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;

namespace RecipeApp.Controllers
{
    public class ShopController : Controller
    {
        private static readonly List<Book> BooksList = new()
        {
            new Book { Id = 1, Title = "C# 12 and .NET 8", Price = 850, Description = "Глибоке занурення у розробку", ImageUrl = "https://via.placeholder.com/150" },
            new Book { Id = 2, Title = "ASP.NET Core in Action", Price = 920, Description = "Все про веб-розробку на MVC", ImageUrl = "https://via.placeholder.com/150" }
        };

        [HttpGet]
        public IActionResult Books()
        {
            return View(BooksList);
        }

        [HttpGet]
        public IActionResult Order(int bookId)
        {
            var book = BooksList.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return NotFound();

            var orderViewModel = new OrderViewModel
            {
                BookId = book.Id,
                BookTitle = book.Title,
                BookPrice = book.Price
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult ProcessOrder([FromBody] OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Заповніть усі поля коректно.");
            }

            return PartialView("_ThankYouPartial", model);
        }
    }
}