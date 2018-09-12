using Common.Conventions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Common.Implementations
{
    public sealed class LinqProvider : ILinqProvider
    {
        private readonly DbContext context;

        public LinqProvider(DbContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class
        {
            var dbset = context.Set<TEntity>();
            return dbset.AsNoTracking().AsQueryable();
        }
    }
}
