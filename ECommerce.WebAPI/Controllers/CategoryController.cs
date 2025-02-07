using ECommerce.Business.Abstarct;
using ECommerce.Entities.Models;
using ECommerce.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.WebAPI.Controllers
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
        [HttpGet("AllCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var items = await _categoryService.GetAllCategories();
            var list = items.Select(p => new CategoryDto
            {
                Name = p.Name
            });
            return Ok(list);
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var item = await _categoryService.GetCategoryById(id);
            if (item == null)
                return NotFound(new { message = "Category was not found!" });

            return Ok(new CategoryDto { Name = item.Name });
        }


        [HttpPost("NewCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto dto)
        {
            if (dto == null || dto.Name.IsNullOrEmpty())
                return BadRequest(new { message = "Category name cannot be empty!" });

            var item = new Category
            {
                Name = dto.Name
            };
            await _categoryService.AddCategory(item);

            return CreatedAtAction(nameof(GetCategory), new { id = item.Id }, new CategoryDto { Name = item.Name });
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
            if (dto == null || dto.Name.IsNullOrEmpty())
                return BadRequest(new { message = "Category name cannot be empty!" });

            var item = await _categoryService.GetCategoryById(id);
            if (item == null)
                return NotFound(new { message = "Category was not found!" });

            item.Name = dto.Name;
            await _categoryService.UpdateCategory(item);

            return NoContent();
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var item = await _categoryService.GetCategoryById(id);
            if (item == null)
                return NotFound(new { message = "Category was not found!" });

            await _categoryService.DeleteCategory(id);
            return NoContent(); 
        }

    }
}
