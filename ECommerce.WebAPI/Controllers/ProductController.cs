using ECommerce.Business.Abstarct;
using ECommerce.Business.Concrete;
using ECommerce.Entities.Models;
using ECommerce.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetProducts()
        {


            var items = await _productService.GetAllProducts();
            var list = items.Select(p => new ProductDto
            {
                Name = p.Name,
                Count = p.Count,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category?.Name
            }).ToList();
            return Ok(list);
        }

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var item = await _productService.GetProductById(id);
            if (item == null)
                return NotFound(new { message = "Product was not found!" });


            var product = new ProductDto
            {
                Name = item.Name,
                Count = item.Count,
                ImageUrl = item.ImageUrl,
                Description = item.Description,
                Price = item.Price,
                CategoryName = item.Category?.Name
            };
            return Ok(product);
        }


        [HttpPost("NewProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Product cannot be empty!" });

            if (string.IsNullOrEmpty(dto.CategoryName))
                return BadRequest(new { message = "Category name cannot be empty!" });

            var categoryId = await _categoryService.GetCategoryByName(dto.CategoryName);
            if (categoryId == 0)
                return BadRequest(new { message = "Category not found!" });

            var item = new Product
            {
                Name = dto.Name,
                Count = dto.Count,
                ImageUrl = dto.ImageUrl,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = categoryId,
            };
            await _productService.AddProduct(item);

            return Ok(item);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Product name cannot be empty!" });

            if (string.IsNullOrEmpty(dto.CategoryName))
                return BadRequest(new { message = "Category name cannot be empty!" });

            var categoryId = await _categoryService.GetCategoryByName(dto.CategoryName);
            if (categoryId == 0)
                return BadRequest(new { message = "Category not found!" });


            var item = await _productService.GetProductById(id);
            if (item == null)
                return NotFound(new { message = "Product was not found!" });

            item.Name = dto.Name;
            item.Count = dto.Count;
            item.ImageUrl = dto.ImageUrl;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.CategoryId = categoryId;
            await _productService.UpdateProduct(item);

            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var item = await _productService.GetProductById(id);
            if (item == null)
                return NotFound(new { message = "Product was not found!" });

            await _productService.DeleteProduct(id);
            return NoContent();
        }

    }
}
