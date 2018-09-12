using Autofac;
using Common.Conventions.Queries;

namespace Common.Autofac.Resolver
{
    public class QueryResolver : IQueryResolver
    {
        private readonly IComponentContext componentContext;

        public QueryResolver(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IQuery<TCriterion, TResult> Resolve<TCriterion, TResult>() 
            where TCriterion : ICriterion
        {
            return componentContext.Resolve<IQuery<TCriterion, TResult>>();
        }

        public IQuery<TCriterion, TEntity, TResult> Resolve<TCriterion, TEntity, TResult>()
            where TCriterion : ICriterion
        {
            return componentContext.Resolve<IQuery<TCriterion, TEntity, TResult>>();
        }
    }
}