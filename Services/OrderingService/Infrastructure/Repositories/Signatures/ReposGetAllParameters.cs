using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Signatures
{
    public class ReposGetAllParameters<TEntity> where TEntity : class
    {
        public string IncludeProperties = "";
        public List<Expression<Func<TEntity, bool>>> Filters;
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null;
        public bool Descending = false;
        public int Page = 0;
        public int PageSize = 10;

        public ReposGetAllParameters(
            string includeProperties,
            List<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            bool descending, int page, int pageSize)
        {
            IncludeProperties = includeProperties;
            Filters = filters;
            OrderBy = orderBy;
            Descending = descending;
            Page = page;
            PageSize = pageSize;
        }
    }
}
