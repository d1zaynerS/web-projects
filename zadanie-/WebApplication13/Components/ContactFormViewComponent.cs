using Microsoft.AspNetCore.Mvc;
using WebApplication13.Models;

namespace WebApplication13.Components
{
    public class ContactFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new ContactMessage());
        }
    }
}