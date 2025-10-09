using Domain.BaseEntity;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : IEntity
    {
        private readonly List<OrderItem> _items = new();
        private Order() { }
        [Key]
        public Guid Id { get; }
        [Required]
        public Guid BuyerId { get; }
        [Required]
        public Guid SellerId { get; }
        [Required]
        public DateTime CreatedAt { get; }
        [Required]
        public DateTime UpdatedAt { get; private set; }
        [Required]
        public OrderStatus Status { get; private set; }

        public Order(Guid buyerId, Guid sellerId, IEnumerable<OrderItem> items)
        {
            Id = Guid.NewGuid();
            BuyerId = buyerId;
            SellerId = sellerId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
            Status = OrderStatus.Pending;
            _items.AddRange(items);
        }
        public IReadOnlyCollection<OrderItem> Items => _items;


    }
}
