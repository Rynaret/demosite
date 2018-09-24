using Common.Conventions;
using Common.Conventions.Commands;
using Common.Models.Contexts;
using People.Models.Contexts;
using System.Threading.Tasks;

namespace People.EventHandlers
{
    public class CreatePersonEventHandler : IIntegrationEventHandler<CreatePersonEventContext>
    {
        private readonly ICommandBuilder commandBuilder;
        private readonly IEventBus eventBus;

        public CreatePersonEventHandler(ICommandBuilder commandBuilder, IEventBus eventBus)
        {
            this.commandBuilder = commandBuilder;
            this.eventBus = eventBus;
        }

        public async Task Handle(CreatePersonEventContext @event)
        {
            var createUser = new CreateUserContext();
            await commandBuilder.ExecuteAsync(createUser);

            var setQuote = new SetQuoteContext(createUser.IdAfterCreate);
            await commandBuilder.ExecuteAsync(setQuote);

            var getPoemEvent = new CreatePoemForPersonEventContext(createUser.IdAfterCreate);
            eventBus.Publish(getPoemEvent);
        }
    }
}
