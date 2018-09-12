using System.Threading.Tasks;

namespace Common.Conventions.Commands
{
    public interface ICommandBuilder
    {
        Task ExecuteAsync<TCommandContext>(TCommandContext commandContext) 
            where TCommandContext : ICommandContext;
    }
}