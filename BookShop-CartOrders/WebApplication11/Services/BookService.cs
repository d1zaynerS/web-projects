using WebApplication11.Models;

namespace WebApplication11.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Кобзар", Author = "Тарас Шевченко", Genre = "Поезія", Year = 1840 },
            new Book { Id = 2, Title = "Тіні забутих предків", Author = "Михайло Коцюбинський", Genre = "Повість", Year = 1911 }
        };

        public IEnumerable<Book> GetAllBooks() => _books;

        public void AddBook(Book book)
        {
            book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
        }
    }
}