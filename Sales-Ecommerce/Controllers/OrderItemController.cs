using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceService.IService;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IOrderService _orderService;
        public OrderItemController(ILogger<OrderItemController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(List<OrderItem> orderItem)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.AddOrderItemAsync(orderItem);
                if (result)
                {
                    return Ok(new { Message = "Order items added successfully." });
                }
                ModelState.AddModelError("", "Failed to add order items.");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetOrderItemsByProductId/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByProductId(int orderId)
        {
            var orderItems = await _orderService.GetOrderItemsByProductIdAsync(orderId);
            if (orderItems != null && orderItems.Any())
            {
                return Ok(orderItems);
            }
            return NotFound($"No order items found for Order ID {orderId}.");
        }

        [HttpGet("GetOrderItemsByUserId/{id}")]
        public async Task<IActionResult> GetOrderItemsByUserId(int id)
        {
            var orderItem = await _orderService.GetOrderItemsByUserIdAsync(id);
            if (orderItem != null)
            {
                return Ok(orderItem);
            }
            return NotFound($"No order items found for User ID {id}.");
        }
        [HttpDelete("DeleteOrderItem/{orderId}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderService.DeleteOrderItemAsync(id);
            if (result)
            {
                return Ok("Order item deleted successfully.");
            }
            return NotFound($"Order item with ID {id} not found.");
        }

    }
}
