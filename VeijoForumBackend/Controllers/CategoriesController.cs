using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models.Dto.CategoryDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();

            if (categories == null || !categories.Any())
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return BadRequest();
            }

            var result = _categoryService.CreateCategory(createCategoryDto);

            return Ok(result);
        }
    }
}
