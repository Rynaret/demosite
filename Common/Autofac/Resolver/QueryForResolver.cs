using Autofac;
using Common.Conventions.Queries;

namespace Common.Autofac.Resolver
{
    public class QueryForResolver : IQueryForResolver
    {
        private readonly IComponentContext componentContext;

        public QueryForResolver(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IQueryFor<T> Resolve<T>()
        {
            return componentContext.Resolve<IQueryFor<T>>();
        }

        public IQueryFor<T, TEntity> ResolveGeneric<T, TEntity>() where TEntity : class
        {
            return componentContext.Resolve<IQueryFor<T, TEntity>>();
        }
    }
}