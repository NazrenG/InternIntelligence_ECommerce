using ECommerce.Business.Abstarct;
using ECommerce.Entities.Models;
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
            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound(new { message = "Cart item is empty!" });
            }
            return Ok(cart.CartItems.ToList());

        }
        [Authorize]
        [HttpPost("NewCartItem")]
        public async Task<IActionResult> AddUserCartItem([FromBody] CartItem dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _cartService.GetCartByUserId(userId);
            var existingCartItem = cart.CartItems?.FirstOrDefault(pi => pi.ProductId == dto.ProductId);
            if (existingCartItem == null)
            {
                cart.CartItems?.Add(dto);
            }

            return Ok(cart);

        }

        [Authorize]
        [HttpDelete("DeletedItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
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
                return NotFound("Cart item not found");
            }
            await _cartItemService.DeleteCartItem(id);  
           
            return Ok(new { message = "Cart item deleted succesfully.", cart });
        }
        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            var cart = await _cartService.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("User card not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            cart.CartItems.Remove(cartItem);
            await _cartService.UpdateCart(cart);    

            return Ok(cart);
        }

    }
}
