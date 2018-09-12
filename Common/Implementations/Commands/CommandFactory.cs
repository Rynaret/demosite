using Common.Conventions.Commands;

namespace Common.Implementations.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ICommandResolver commandResolver;

        public CommandFactory(ICommandResolver commandResolver)
        {
            this.commandResolver = commandResolver;
        }

        public ICommand<TCommandContext> Create<TCommandContext>() 
            where TCommandContext : ICommandContext
        {
            return commandResolver.Resolve<TCommandContext>();
        }
    }
}