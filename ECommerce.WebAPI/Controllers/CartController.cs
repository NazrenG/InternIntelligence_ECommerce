using ECommerce.Business.Abstract;
using ECommerce.WebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public CartController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }
        [Authorize]
        [HttpGet("UserCartItems")]
        public async Task<IActionResult> GetUserCartItem()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found!" });
            }

            var cartItems = await _cartItemService.GetAllItemsForCartId(cart.Id);
            var list = cartItems.Select(c => new ProductDto
            {
                Name = c.Product?.Name,
                Price = c.Product.Price,
                CategoryName = c.Product.Category.Name,
                Count = c.Product.Count,
                Description = c.Product.Description,
                ImageUrl = c.Product.ImageUrl,
            }).ToList();
            return Ok(list);
        }

        [Authorize]
        [HttpPost("NewCartItem/{productId}")]
        public async Task<IActionResult> AddUserCartItem(int productId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var existingCartItem = cart.CartItems?.FirstOrDefault(pi => pi.ProductId == productId);
            if (existingCartItem != null)
            {
                return BadRequest(new { message = "Item already exists in the cart." });
            }

            var item = new Entities.Models.CartItem { CartId = cart.Id, ProductId = productId };
            await _cartItemService.AddCartItem(item);

            return Ok(new { message = "Cart item created successfully!" });
        }


        [Authorize]
        [HttpDelete("DeletedItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }

            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("User cart not found.");
            }

            var item = await _cartItemService.GetCartItemById(id);
            if (item == null || item.CartId != cart.Id)
            {
                return NotFound("Cart item not found.");
            }

            await _cartItemService.DeleteCartItem(id);
            return Ok(new { message = "Cart item deleted successfully." });
        }



    }
}
