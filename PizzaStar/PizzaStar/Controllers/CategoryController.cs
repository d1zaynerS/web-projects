using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaStar.Interfaces;
using PizzaStar.Models;
using PizzaStar.Models.Pages;

namespace PizzaStar.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    [Route("panel/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepository;

        public CategoryController(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index(QueryOptions options)
        {
            var categories = _categoryRepository.GetAllCategories(options);
            return View(categories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.DateOfPublication = DateTime.Now;
                await _categoryRepository.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _categoryRepository.EditCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteCategoryAsync(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}