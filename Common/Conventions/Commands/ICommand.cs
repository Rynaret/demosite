using System.Threading.Tasks;

namespace Common.Conventions.Commands
{
    public interface ICommand<in TCommandContext> 
        where TCommandContext : ICommandContext
    {
        Task ExecuteAsync(TCommandContext commandContext);
    }

    public interface ICommand<in TCommandContext, TEntity, TModel>
        where TCommandContext : ICommandContext
        where TEntity : class
        where TModel : class
    {
        Task ExecuteAsync(TCommandContext commandContext);
    }
}