using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]

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

            item.Name = string.IsNullOrEmpty(dto.Name)?item.Name:dto.Name;
            item.Count = dto.Count;
            item.ImageUrl = string.IsNullOrEmpty(dto.ImageUrl) ? item.ImageUrl : dto.ImageUrl;
            item.Description = string.IsNullOrEmpty(dto.Description) ? item.Description : dto.Description; ;
            item.Price = dto.Price;
            item.CategoryId = categoryId;
            await _productService.UpdateProduct(item);

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdatedProductCount/{id}")]
        public async Task<IActionResult> UpdateProductCount(int id, [FromBody] bool checkCountChange)
        {
            var item = await _productService.GetProductById(id);
            if (item == null)
                return NotFound(new { message = "Product was not found!" });
          await _productService.ChangeCount(checkCountChange,id);   
            return NoContent();
        }


        [Authorize(Roles = "Admin")]

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var item = await _productService.GetProductById(id);
            if (item == null)
                return NotFound(new { message = "Product was not found!" });

            await _productService.DeleteProduct(id);
            return NoContent();
        }


        [HttpGet("ProductByCategoryName")]
        public async Task<IActionResult> GetProductByCategoryName(string category)
        {

            var caytegoryId=await _categoryService.GetCategoryByName(category);
            var items = await _productService.GetProductByCategoryId(caytegoryId);
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

    }
}
