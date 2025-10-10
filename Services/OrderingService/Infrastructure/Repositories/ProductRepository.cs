using Domain.Entities.Values;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {

        private readonly OrderDbContext _db;
        public ProductRepository(OrderDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task UpsertAsync(Product product)
        {
            var exists = await _db.Products.FindAsync(product.Id);
            if (exists == null)
            {
                await _db.Products.AddAsync(product);
            }
            else
            {
                exists.Update(product.Title, product.Price, product.IsActive);
                _db.Products.Update(exists);
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId)
        {
            var p = await _db.Products.FindAsync(productId);
            if (p != null)
            {
                _db.Products.Remove(p);
                await _db.SaveChangesAsync();
            }
        }
    }
}
