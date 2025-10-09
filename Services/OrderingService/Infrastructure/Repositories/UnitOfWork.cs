using Domain.Entities;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public readonly OrderDbContext Context;
        private readonly IServiceProvider _provider;

        public UnitOfWork(OrderDbContext context, IServiceProvider provider)
        {
            Context = context;
            _provider = provider;
        }
        public OrderRepository Order => _provider.GetRequiredService<OrderRepository>();
        
        public async Task SaveAsync(CancellationToken ct = default) => await Context.SaveChangesAsync(ct);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
        => await Context.Database.BeginTransactionAsync(ct);

        public async Task CommitTransactionAsync(IDbContextTransaction tx, CancellationToken ct = default)
            => await tx.CommitAsync(ct);

        public async Task RollbackTransactionAsync(IDbContextTransaction tx, CancellationToken ct = default)
            => await tx.RollbackAsync(ct);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}
