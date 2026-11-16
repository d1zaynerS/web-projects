using Microsoft.AspNetCore.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Components
{
    public class GalleryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var images = new List<ImageModel>
            {
                new ImageModel { Id = 1, Title = "Природа", Description = "Гарний пейзаж", FilePath = "/images/nature.jpg" },
                new ImageModel { Id = 2, Title = "Місто", Description = "Нічне місто", FilePath = "/images/city.jpg" },
                new ImageModel { Id = 3, Title = "Гори", Description = "Високі гори", FilePath = "/images/mountains.jpg" }
            };

            return View(images);
        }
    }
}