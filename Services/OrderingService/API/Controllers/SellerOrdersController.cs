using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/seller/orders")]
    public class SellerOrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public SellerOrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{sellerId}")]
        public async Task<IActionResult> GetSellerOrders(Guid sellerId)
        {
            var orders = await _orderService.GetSellerOrders(sellerId);
            return Ok(orders);
        }
    }
}
