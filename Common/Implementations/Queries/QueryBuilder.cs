using Common.Conventions.Queries;

namespace Common.Implementations.Queries
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly IQueryForResolver queryForResolver;

        public QueryBuilder(IQueryForResolver queryForResolver)
        {
            this.queryForResolver = queryForResolver;
        }

        public IQueryFor<TResult> For<TResult>()
        {
            return queryForResolver.Resolve<TResult>();
        }

        public IQueryFor<TResult, TEntity> For<TEntity, TResult>() where TEntity : class
        {
            return queryForResolver.ResolveGeneric<TResult, TEntity>();
        }
    }
}