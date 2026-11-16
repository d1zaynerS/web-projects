using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationContext _context;

        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Images).ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Images)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int bookId, string authorName, string text)
        {
            var comment = new Comment
            {
                BookId = bookId,
                AuthorName = authorName,
                Text = text,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = bookId });
        }
    }
}