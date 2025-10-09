using Domain.BaseEntity;
using Domain.Entities;
using Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.Values
{
    [Table("Order")]
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

        public void MarkAsPaid()
        {
            Status = OrderStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsShipped()
        {
            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDelivered()
        {
            Status = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReplaceItems(IEnumerable<OrderItem> items)
        {
            _items.Clear();
            _items.AddRange(items);
            UpdatedAt = DateTime.UtcNow;
        }

        public void CancelDueToProductDeletion()
        {
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProductPrice(Guid productId, decimal newPrice)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null) item.UpdatePrice(newPrice);
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
