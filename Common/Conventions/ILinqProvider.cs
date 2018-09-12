using System.Linq;

namespace Common.Conventions
{
    public interface ILinqProvider
    {
        IQueryable<TEntity> Query<TEntity>()
            where TEntity : class;
    }
}
