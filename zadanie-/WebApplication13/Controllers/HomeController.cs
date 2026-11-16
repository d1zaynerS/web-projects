using Microsoft.AspNetCore.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
            SeedData();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactMessage contactMessage)
        {
            return RedirectToAction("Thanks", new { name = contactMessage.Name });
        }

        public IActionResult Thanks(string name)
        {
            ViewBag.UserName = name;
            return View();
        }

        public IActionResult Products(string category, decimal? maxPrice, bool? isAvailable, string sortBy)
        {
            ViewBag.Category = category;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.IsAvailable = isAvailable;
            ViewBag.SortBy = sortBy;
            return View();
        }

        private void SeedData()
        {
            if (!_context.Books.Any())
            {
                var book = new Book
                {
                    Title = "Кобзар",
                    Author = "Тарас Шевченко",
                    Genre = "Поезія",
                    Price = 350,
                    Images = new List<BookImage>
                    {
                        new BookImage { FilePath = "/images/kobzar1.jpg" },
                        new BookImage { FilePath = "/images/kobzar2.jpg" }
                    }
                };
                _context.Books.Add(book);
            }

            if (!_context.Products.Any())
            {
                _context.Products.AddRange(
                    new Product { Name = "Ноутбук", Description = "Потужний ноутбук", Price = 25000, Category = "Електроніка", IsAvailable = true, Popularity = 5 },
                    new Product { Name = "Смартфон", Description = "Сучасний телефон", Price = 15000, Category = "Електроніка", IsAvailable = true, Popularity = 4 },
                    new Product { Name = "Крісло", Description = "Зручне офісне крісло", Price = 4500, Category = "Меблі", IsAvailable = true, Popularity = 3 }
                );
            }

            _context.SaveChanges();
        }
    }
}