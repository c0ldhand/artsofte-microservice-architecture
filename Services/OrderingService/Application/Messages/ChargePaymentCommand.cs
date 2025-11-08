using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public record ChargePaymentCommand(Guid OrderId, Guid BuyerId, decimal Amount);
}
