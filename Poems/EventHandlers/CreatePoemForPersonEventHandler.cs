using Common.Conventions;
using Common.Conventions.Commands;
using Common.Models.Contexts;
using Poems.Models.Contexts;
using System.Threading.Tasks;

namespace Poems.EventHandlers
{
    public class CreatePoemForPersonEventHandler : IIntegrationEventHandler<CreatePoemForPersonEventContext>
    {
        private readonly ICommandBuilder commandBuilder;

        public CreatePoemForPersonEventHandler(ICommandBuilder commandBuilder)
        {
            this.commandBuilder = commandBuilder;
        }

        public async Task Handle(CreatePoemForPersonEventContext @event)
        {
            var createPoem = new CreatePoemContext(@event.PersonId);
            await commandBuilder.ExecuteAsync(createPoem);

            var estimatePoem = new EstimatePoemContext(createPoem.IdAfterCreate);
            await commandBuilder.ExecuteAsync(estimatePoem);
        }
    }
}
