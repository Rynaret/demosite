using Common.Conventions;
using Common.Conventions.Commands;
using Common.Models.Contexts;
using People.Models.Contexts;
using System.Threading.Tasks;

namespace People.EventHandlers
{
    public class PeopleGetInfoEventHandler : IIntegrationEventHandler<PeopleGetInfoEventContext>
    {
        private readonly ICommandBuilder commandBuilder;
        private readonly IEventBus eventBus;

        public PeopleGetInfoEventHandler(ICommandBuilder commandBuilder, IEventBus eventBus)
        {
            this.commandBuilder = commandBuilder;
            this.eventBus = eventBus;
        }

        public async Task Handle(PeopleGetInfoEventContext @event)
        {
            var createUser = new CreateUserContext();
            await commandBuilder.ExecuteAsync(createUser);

            var setQuote = new SetQuoteContext(createUser.IdAfterCreate);
            await commandBuilder.ExecuteAsync(setQuote);

            var getPoemEvent = new GetPoemEventContext(createUser.IdAfterCreate);
            eventBus.Publish(getPoemEvent);
        }
    }
}
