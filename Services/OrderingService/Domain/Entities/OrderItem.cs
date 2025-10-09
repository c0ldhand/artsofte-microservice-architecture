using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem : IEntity
    {
        private OrderItem()
        {
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; private set; }
        [Required]
        public int Quantity { get; private set; }
        [Required]
        public decimal Price { get; private set; }
        public OrderItem(Guid productId, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        

        

    }
}
