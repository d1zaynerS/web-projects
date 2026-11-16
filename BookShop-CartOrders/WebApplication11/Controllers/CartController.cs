using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;
using WebApplication11.ViewModel;

namespace WebApplication11.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            ViewBag.CartCount = _cartService.Cart.Sum(i => i.Quantity);
            return View(_cartService.Products);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new OrderViewModel
            {
                Items = _cartService.Cart
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            model.Items = new List<CartItem>(_cartService.Cart);
            _cartService.ClearCart();
            return View("OrderResult", model);
        }
    }
}   