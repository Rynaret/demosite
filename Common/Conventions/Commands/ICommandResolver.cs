namespace Common.Conventions.Commands
{
    public interface ICommandResolver
    {
        ICommand<TCommandContext> Resolve<TCommandContext>()
            where TCommandContext : ICommandContext;

        ICommand<TCommandContext, TEntity, TModel> Resolve<TCommandContext, TEntity, TModel>()
            where TEntity : class
            where TModel : class
            where TCommandContext : ICommandContext;
    }
}