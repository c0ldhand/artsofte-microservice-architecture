using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public record CreateOrderRequest(Guid OrderId, Guid BuyerId, Guid ProductId, int Quantity, decimal Amount);
}
