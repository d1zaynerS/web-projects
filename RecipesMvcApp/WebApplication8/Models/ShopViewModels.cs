namespace RecipeApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class OrderViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public decimal BookPrice { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string NovaPoshtaBranch { get; set; } = string.Empty;
    }
}