using Common.Conventions.Queries;

namespace Common.Implementations.Queries
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IQueryResolver queryResolver;

        public QueryFactory(IQueryResolver queryResolver)
        {
            this.queryResolver = queryResolver;
        }

        public IQuery<TCriterion, TResult> Create<TCriterion, TResult>() 
            where TCriterion : ICriterion
        {
            return queryResolver.Resolve<TCriterion, TResult>();
        }

        public IQuery<TCriterion, TEntity, TResult> Create<TCriterion, TEntity, TResult>()
            where TCriterion : ICriterion
        {
            return queryResolver.Resolve<TCriterion, TEntity, TResult>();
        }
    }
}