using Application.Activities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class RoutingSlipCreatedHandler : IConsumer<IRoutingSlipCreated>
    {
        public Task Consume(ConsumeContext<IRoutingSlipCreated> context)
        {
            Console.WriteLine($"Routing Slip created: {context.Message.RoutingSlipId}");
            return Task.CompletedTask;
        }
    }
}
