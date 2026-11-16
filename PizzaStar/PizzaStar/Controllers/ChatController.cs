using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Data;

namespace PizzaStar.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationContext _context;

        public ChatController(ApplicationContext context)
        {
            _context = context;
        }

        [Route("chat")]
        public async Task<IActionResult> Index()
        {

            var messages = await _context.Messages
                .Where(m => m.ReceiverId == null)
                .Include(m => m.Sender)
                .OrderBy(m => m.SentAt)
                .Take(50)
                .ToListAsync();


            ViewBag.Users = await _context.Users.ToListAsync();

            return View(messages);
        }
    }
}