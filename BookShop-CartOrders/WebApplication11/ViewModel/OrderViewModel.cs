using WebApplication11.Models;

namespace WebApplication11.ViewModel
{
    public class OrderViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalSum => Items.Sum(i => i.Product.Price * i.Quantity);
    }
}