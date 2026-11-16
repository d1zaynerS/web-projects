namespace WebApplication13.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public List<BookImage> Images { get; set; } = new List<BookImage>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}