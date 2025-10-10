using Application.DTO;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly OrderService _orderService;

        public OrdersController(UnitOfWork unitOfWork, OrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto)
        {
            var id = await _orderService.CreateOrder(dto.BuyerId, dto.SellerId,
            dto.Items.Select(i => (i.ProductId, i.Quantity, i.Price)).ToList());
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusDTO dto)
        {
            await _orderService.UpdateStatus(id, dto.Status);
            return NoContent();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItems(Guid id, [FromBody] UpdateItemsDTO dto)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null) return NotFound();
            await _orderService.UpdateOrderItemsAsync(id, dto.Items);
            return NoContent();
        }
    }
}
