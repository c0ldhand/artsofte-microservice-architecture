using Domain.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Values
{
    [Table("OrderItem")]
    public class OrderItem : IEntity
    {
        private OrderItem()
        {
        }

        [Key]
        public Guid Id { get;}
        [Required]
        public Guid ProductId { get; }
        [Required]
        public int Quantity { get; }
        [Required]
        public decimal Price { get; private set; }
        public OrderItem(Guid productId, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }



    }
}
