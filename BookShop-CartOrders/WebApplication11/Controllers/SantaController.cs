using Microsoft.AspNetCore.Mvc;
using WebApplication11.Services;

namespace WebApplication11.Controllers
{
    public class SantaController : Controller
    {
        private readonly SantaService _santaService;

        public SantaController(SantaService santaService)
        {
            _santaService = santaService;
        }

        public IActionResult Index()
        {
            var pairs = _santaService.DrawSantas();
            return View(pairs);
        }
    }
}