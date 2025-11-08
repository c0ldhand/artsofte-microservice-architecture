using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public record PaymentAccepted(Guid OrderId, Guid BuyerId, decimal Amount);
}
