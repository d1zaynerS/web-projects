namespace WebApplication13.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}