using Common.Conventions.Queries;
using System.Threading.Tasks;

namespace Common.Implementations.Queries
{
    public class QueryFor<TResult> : IQueryFor<TResult>
    {
        private readonly IQueryFactory factory;

        public QueryFor(IQueryFactory factory)
        {
            this.factory = factory;
        }

        public async Task<TResult> WithAsync<TCriterion>(TCriterion criterion) 
            where TCriterion : ICriterion
        {
            return await factory.Create<TCriterion, TResult>().AskAsync(criterion);
        }
    }

    public class QueryFor<TResult, TEntity> : IQueryFor<TResult, TEntity>
            where TEntity : class
    {
        private readonly IQueryFactory factory;

        public QueryFor(IQueryFactory factory)
        {
            this.factory = factory;
        }

        public async Task<TResult> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            return await factory.Create<TCriterion, TEntity, TResult>().AskAsync(criterion);
        }
    }
}