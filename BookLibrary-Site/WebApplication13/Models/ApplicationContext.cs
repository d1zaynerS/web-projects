using Microsoft.EntityFrameworkCore;

namespace WebApplication13.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageModel> GalleryImages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}