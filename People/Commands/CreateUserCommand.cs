using AutoMapper;
using Common.Conventions;
using Common.Conventions.Commands;
using Common.Conventions.Queries;
using People.Models.Contexts;
using People.Models.Criterions;
using People.Models.ExternalJsonModels;
using System.Linq;
using System.Threading.Tasks;

namespace People.Commands
{
    public class CreateUserCommand : ICommand<CreateUserContext>
    {
        private readonly IQueryBuilder queryBuilder;
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public CreateUserCommand(IQueryBuilder queryBuilder, IMapper mapper, IRepository repository)
        {
            this.queryBuilder = queryBuilder;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task ExecuteAsync(CreateUserContext commandContext)
        {
            var getRandomUser = new GetRandomUserCriterion();
            var randomUser = await queryBuilder.For<RandomUserResultModel>().WithAsync(getRandomUser);

            var entity = mapper.Map<Entities.People>(randomUser.Results.First());

            await repository.AddAsync(entity);
            await repository.SaveAsync();

            commandContext.IdAfterCreate = entity.Id;
        }
    }
}
