using AutoMapper;
using Common.Conventions;
using Common.Conventions.Commands;
using Common.Conventions.Queries;
using Poems.Entities;
using Poems.Models.Contexts;
using Poems.Models.Criterions;
using Poems.Models.ExternalJsonModels;
using System.Linq;
using System.Threading.Tasks;

namespace Poems.Commands
{
    public class CreatePoemCommand : ICommand<CreatePoemContext>
    {
        private readonly IQueryBuilder queryBuilder;
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public CreatePoemCommand(IQueryBuilder queryBuilder, IMapper mapper, IRepository repository)
        {
            this.queryBuilder = queryBuilder;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task ExecuteAsync(CreatePoemContext commandContext)
        {
            var getRandomPoem = new GetRandomPoemCriterion();
            var randomPoem = await queryBuilder.For<RandomPoemResultModel[]>().WithAsync(getRandomPoem);

            var entity = mapper.Map<Poem>(randomPoem.First());

            entity.PoetId = commandContext.PeopleId;
            await repository.AddAsync(entity);
            await repository.SaveAsync();

            commandContext.IdAfterCreate = entity.Id;
        }
    }
}
