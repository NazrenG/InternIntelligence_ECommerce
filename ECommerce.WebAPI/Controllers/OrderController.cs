using ECommerce.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
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

            var orders = await _orderService.GetOrdersByUserId(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found!" });
            }

            var items = orders.Select(o => o.Items).ToList();
            if (!items.Any())
            {
                return NotFound(new { message = "No items found in the orders!" });
            }

            return Ok(items);
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

            var orders = await _orderService.GetOrdersByUserId(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found!" });
            }

            var total = orders.SelectMany(o => o.Items).Sum(item => item.Price * item.Count);
            return Ok(total);
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

            var item = await _orderItemService.GetOrderItemById(id);
            if (item == null)
            {
                return NotFound(new { message = "Order item not found!" });
            }
            await _orderItemService.ChangeCount(checkChange,id);
            
            return Ok(new { message = "Item count updated successfully.", item.Count });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("OrderStatus/{id}")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] string status)
        {
            var validStatuses = new List<string> { "pending", "ready", "shipped", "cancelled" };
            if (string.IsNullOrWhiteSpace(status) || !validStatuses.Contains(status.ToLower()))
            {
                return BadRequest(new { message = "Invalid status. Valid statuses: pending, ready, shipped, cancelled." });
            }

            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found!" });
            }

            order.Status = status;
            await _orderService.UpdateOrder(order);

            return Ok(new { message = "Order status updated successfully.", order.Status });
        }

    }
}
  