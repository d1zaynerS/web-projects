using WebApplication11.Models;

namespace WebApplication11.Services
{
    public class CartService
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ноутбук", Price = 25000, StockQuantity = 10 },
            new Product { Id = 2, Name = "Смартфон", Price = 12000, StockQuantity = 15 },
            new Product { Id = 3, Name = "Навушники", Price = 1500, StockQuantity = 30 }
        };

        private readonly List<CartItem> _cart = new List<CartItem>();

        public IEnumerable<Product> Products => _products;
        public List<CartItem> Cart => _cart;

        public void AddToCart(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var item = _cart.FirstOrDefault(c => c.Product.Id == productId);
                if (item != null)
                {
                    item.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItem { Product = product, Quantity = 1 });
                }
            }
        }

        public void ClearCart()
        {
            _cart.Clear();
        }
    }
}