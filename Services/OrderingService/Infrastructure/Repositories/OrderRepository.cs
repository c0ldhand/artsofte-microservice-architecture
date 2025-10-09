using Domain.Entities;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        private readonly OrderDbContext _db;
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, ct);
        }
        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Order>> GetByBuyerAsync(Guid buyerId)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .Where(o => o.BuyerId == buyerId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetBySellerAsync(Guid sellerId)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .Where(o => o.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByProductAsync(Guid productId)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .Where(o => o.Items.Any(i => i.ProductId == productId))
                .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }
    }
}
