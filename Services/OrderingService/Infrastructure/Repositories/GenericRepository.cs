using Domain.BaseEntity;
using Infrastructure.EF;
using Infrastructure.Repositories.Signatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        public readonly DbSet<TEntity> DbSet;

        public GenericRepository(OrderDbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<ReposGetAllReturn<TEntity>> GetAll(ReposGetAllParameters<TEntity> dataParams)
        {
            IQueryable<TEntity> query = DbSet;
            ApplyParameters(query, dataParams);

            int totalCount = await query.CountAsync();
            int pageCount;
            if (dataParams.PageSize == -1)
                pageCount = totalCount;
            else
            {
                pageCount = await GetWithTotal(query, dataParams.PageSize);
                query = query.Skip(dataParams.Page * dataParams.PageSize).Take(dataParams.PageSize);
            }

            return new ReposGetAllReturn<TEntity>(await query.ToListAsync(), pageCount, totalCount);
        }

        private static void ApplyParameters(IQueryable<TEntity> query, ReposGetAllParameters<TEntity> dataParams)
        {
            foreach (var property in dataParams.IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            foreach (var filter in dataParams.Filters)
                query = query.Where(filter);

            if (dataParams.OrderBy != null)
                query = dataParams.OrderBy(query);

            if (dataParams.Descending)
                query = dataParams.Descending
                    ? query.OrderByDescending(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, "Id"))
                    : query.OrderBy(e => Microsoft.EntityFrameworkCore.EF.Property<object>(e, "Id"));
        }

        public virtual async Task<List<TEntity>> GetAll() =>
            await DbSet.ToListAsync();

        private static async Task<int> GetWithTotal(IQueryable<TEntity> query, int pageSize) =>
            (int)Math.Ceiling(await query.CountAsync() * 1.0 / pageSize);

        public virtual async Task<TEntity?> Retrieve(Expression<Func<TEntity, bool>> predicate,
            string? includeProperties = null)
        {
            if (includeProperties == null)
                return await DbSet.FirstOrDefaultAsync(predicate);

            IQueryable<TEntity> query = DbSet;

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            var str = query.ToQueryString();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual Task Create(TEntity entity) =>
            Task.FromResult(DbSet.Add(entity));

        public virtual Task Update(TEntity entity) =>
            Task.FromResult(DbSet.Update(entity));

        public virtual void UpdateRange(IEnumerable<TEntity> entities) =>
            DbSet.UpdateRange(entities);

        public virtual async Task AddRange(IEnumerable<TEntity> entities) =>
            await DbSet.AddRangeAsync(entities);

        public virtual Task Delete(TEntity entity) =>
            Task.FromResult(DbSet.Remove(entity));

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.CountAsync(predicate);

        public virtual async Task<int> Count() => await DbSet.CountAsync();

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default) =>
            await DbSet.AnyAsync(predicate, ct);

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter) =>
            await DbSet.Where(filter).ToListAsync();

        public IQueryable<TEntity> GetQueryable() => DbSet.AsQueryable();
    }
}
