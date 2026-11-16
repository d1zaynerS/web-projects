using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;

namespace RecipeApp.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly Dictionary<string, decimal> _rates = new()
        {
            { "USD", 41.2m },
            { "EUR", 45.0m },
            { "UAH", 1.0m }
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CurrencyViewModel());
        }

        [HttpPost]
        public IActionResult Index(CurrencyViewModel model)
        {
            if (model.Amount > 0 && _rates.ContainsKey(model.SellCurrency) && _rates.ContainsKey(model.BuyCurrency))
            {
                decimal amountInUah = model.Amount * _rates[model.SellCurrency];
                model.ResultAmount = amountInUah / _rates[model.BuyCurrency];
            }
            return View(model);
        }
    }
}