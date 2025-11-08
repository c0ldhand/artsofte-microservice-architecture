using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sagas
{
    public class OrderState : SagaStateMachineInstance
    {
        public string CurrentState { get; set; }
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
        public bool InventoryReserved { get; set; }
        public bool PaymentAccepted { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
