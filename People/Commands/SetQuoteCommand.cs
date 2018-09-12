using Common.Conventions;
using Common.Conventions.Commands;
using Common.Conventions.Queries;
using People.Models.Contexts;
using People.Models.Criterions;
using System.Threading.Tasks;

namespace People.Commands
{
    public class SetQuoteCommand : ICommand<SetQuoteContext>
    {
        private readonly IQueryBuilder queryBuilder;
        private readonly IRepository repository;

        public SetQuoteCommand(IQueryBuilder queryBuilder, IRepository repository)
        {
            this.queryBuilder = queryBuilder;
            this.repository = repository;
        }

        public async Task ExecuteAsync(SetQuoteContext commandContext)
        {
            var getRandomQuote = new GetRandomQuoteCriterion();
            var quote = await queryBuilder.For<string>().WithAsync(getRandomQuote);

            var entity = await repository.GetAsync<Entities.People>(commandContext.PeopleId);
            entity.Quote = quote;

            await repository.SaveAsync();
        }
    }
}
