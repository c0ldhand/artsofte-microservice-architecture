using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Values
{
    [Table("Product")]
    public class Product : IEntity
    {
        private Product() { }
        [Key]
        public Guid Id { get; }
        [Required]
        public Guid SellerId { get; }
        [Required]
        public string? Title { get; private set; }
        [Required]
        public decimal Price { get; private set; }
        [Required]
        public bool IsActive { get; private set; }
        [Required]
        public DateTime UpdatedAt { get; private set; }

        public Product(Guid id, Guid sellerId, string? title, decimal price, bool isActive)
        {
            Id = id;
            SellerId = sellerId;
            Title = title;
            Price = price;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string? title, decimal price, bool isActive)
        {
            Title = title;
            Price = price;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
