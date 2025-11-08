using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages
{
    public class RefundPaymentCommand
    {
        public RefundPaymentCommand(Guid orderId, Guid buyerId, decimal amount)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            Amount = amount;
        }
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public decimal Amount { get; set; }
    }
}
