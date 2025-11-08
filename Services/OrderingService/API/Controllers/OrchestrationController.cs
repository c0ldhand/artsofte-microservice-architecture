using Application.Activities;
using Application.Messages;
using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orchestration")]
    public class OrchestrationController : ControllerBase


    {
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publisher;

        public OrchestrationController(IPublishEndpoint publisher, IBus bus)
        {
            _publisher = publisher;
            _bus = bus;
        }
        [HttpPost("coordinator")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Coordinator([FromBody] CreateOrderRequest req)
        {
            await _publisher.Publish(req);
            return Accepted(new { message = "Order request sent to saga coordinator" });
        }

        [HttpPost("orchestrator")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Orchestrator([FromBody] CreateOrderRequest req)
        {
            var builder = new RoutingSlipBuilder(req.OrderId);
            var invArgs = new ReserveInventoryArguments(req.OrderId, req.ProductId, req.Quantity);
            var paymentArgs = new ChargePaymentArguments(req.OrderId, req.BuyerId, req.Amount);
            builder.AddActivity(
                "ReserveInventory",
                new Uri("queue:reserve-inventory_execute"),
                invArgs
            );
            builder.AddActivity(
                "ChargePayment",
                new Uri("queue:charge-payment_execute"),
                paymentArgs
            );
            builder.AddSubscription(
                new Uri("queue:routing-slip-created"),
                RoutingSlipEvents.Completed | RoutingSlipEvents.Faulted,
                x => Task.CompletedTask);

            var routingSlip = builder.Build();
            await _bus.Execute(routingSlip);

            return Accepted(new { message = "Routing Slip started", orderId = req.OrderId });
        }
    }
}
