using Microsoft.AspNetCore.Mvc;
using Sales_EcommerceModel.DbModel;
using Sales_EcommerceService.IService;

namespace Sales_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.PlaceOrderAsync(order);
                if (result)
                {
                    return Ok(new { Message = "Order created successfully." });
                }
                ModelState.AddModelError("", "Failed to create order.");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders != null && orders.Count > 0)
            {
                return Ok(orders);
            }
            return NotFound("No orders found.");
        }
        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with ID {id} not found.");
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (result)
            {
                return Ok("Order deleted successfully.");
            }
            return NotFound($"Order with ID {id} not found.");
        }
    }
}
