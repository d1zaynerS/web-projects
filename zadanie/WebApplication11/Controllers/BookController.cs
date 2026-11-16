using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;
using WebApplication11.ViewModel;

namespace WebApplication11.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            return View(_bookService.GetAllBooks());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                _bookService.AddBook(new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    Year = model.Year
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}