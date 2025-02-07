using ECommerce.Business.Abstarct;
using ECommerce.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [Authorize]
        [HttpGet("OrderItems")]
        public async Task<IActionResult> GetOrderItems()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found!" });
            }

            var order = await _orderService.GetOrdersByUserId(userId);
            if (order == null || order.Items == null || !order.Items.Any())
            {
                return NotFound(new { message = "No items found in the order!" });
            }

            return Ok(order.Items.ToList());
        }
        [Authorize]
        [HttpGet("TotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found!" });
            }

            var order = await _orderService.GetOrdersByUserId(userId);
            if (order == null || order.Items == null || !order.Items.Any())
            {
                return NotFound(new { message = "Order item is empty!" });
            }

            var count = order.Items.Sum(p => p.Price * p.Count);
            return Ok(count);
        }


        [Authorize]
        [HttpPut("OrderItemCount/{id}")]
        public async Task<IActionResult> ChangeItemCount(int id, [FromBody] bool checkChange)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found!" });
            }

            var order = await _orderService.GetOrdersByUserId(userId);
            if (order == null || order.Items == null || !order.Items.Any())
            {
                return NotFound(new { message = "Order item is empty!" });
            }

            await _orderItemService.ChangeCount(checkChange, id);
            return Ok(new { message = "Item count updated successfully." });
        }

        [Authorize]
        [HttpPut("OrderStatus/{id}")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { message = "Status cannot be empty." });
            }

            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found!" });
            }

            order.Status = status;
              await _orderService.UpdateOrder(order);

            

            return Ok(new { message = "Order status updated successfully." });
        }
    }
}
  