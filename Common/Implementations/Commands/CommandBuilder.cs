using Common.Conventions.Commands;
using System.Threading.Tasks;

namespace Common.Implementations.Commands
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly ICommandFactory commandFactory;
        
        public CommandBuilder(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public Task ExecuteAsync<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext
        {
            return commandFactory.Create<TCommandContext>().ExecuteAsync(commandContext);
        }
    }
}