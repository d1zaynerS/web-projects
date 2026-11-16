using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Models;

namespace WebApplication13.Components
{
    public class ProductFilterViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;

        public ProductFilterViewComponent(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string category, decimal? maxPrice, bool? isAvailable, string sortBy)
        {
            IQueryable<Product> products = _context.Products;

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            if (maxPrice.HasValue && maxPrice.Value > 0)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            if (isAvailable.HasValue && isAvailable.Value)
            {
                products = products.Where(p => p.IsAvailable);
            }

            products = sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "popularity" => products.OrderByDescending(p => p.Popularity),
                _ => products.OrderBy(p => p.Name)
            };

            ViewBag.Categories = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();

            return View(await products.ToListAsync());
        }
    }
}