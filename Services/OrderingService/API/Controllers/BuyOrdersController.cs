using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/buyer/orders")]
    public class BuyOrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public BuyOrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{buyerId}")]
        public async Task<IActionResult> GetBuyerOrders(Guid buyerId)
        {
            var orders = await _orderService.GetBuyerOrders(buyerId);
            return Ok(orders);
        }
    }
}
