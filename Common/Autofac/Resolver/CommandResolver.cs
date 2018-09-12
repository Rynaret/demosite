using Autofac;
using Common.Conventions.Commands;

namespace Common.Autofac.Resolver
{
    public class CommandResolver : ICommandResolver
    {
        private readonly IComponentContext componentContext;

        public CommandResolver(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public ICommand<TCommandContext> Resolve<TCommandContext>() 
            where TCommandContext : ICommandContext
        {
            return componentContext.Resolve<ICommand<TCommandContext>>();
        }

        public ICommand<TCommandContext, TEntity, TModel> Resolve<TCommandContext, TEntity, TModel>()
            where TCommandContext : ICommandContext
            where TEntity : class
            where TModel : class
        {
            return componentContext.Resolve<ICommand<TCommandContext, TEntity, TModel>>();
        }
    }
}