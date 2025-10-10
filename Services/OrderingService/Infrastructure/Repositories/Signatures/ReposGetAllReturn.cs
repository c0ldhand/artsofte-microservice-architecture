using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Signatures
{
    public class ReposGetAllReturn<TEntity>(
       List<TEntity> entities, int pageCount, int totalCount
       ) where TEntity : class
    {
        public List<TEntity> Entities = entities;
        public int PageCount = pageCount;
        public int TotalCount = totalCount;
    }
}
