using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }
    }
}
