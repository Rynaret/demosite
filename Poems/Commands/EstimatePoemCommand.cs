using Common.Conventions;
using Common.Conventions.Commands;
using Poems.Entities;
using Poems.Models.Contexts;
using Search;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Poems.Commands
{
    public class EstimatePoemCommand : ICommand<EstimatePoemContext>
    {
        private readonly IRepository repository;

        public EstimatePoemCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(EstimatePoemContext commandContext)
        {
            var poem = await repository.GetAsync<Poem>(commandContext.PoemId);

            var lines = poem.Content.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var linesCount = lines.Count();
            double distance = 0;
            for (int i = 0; i < linesCount - 1; i++)
            {
                var s1 = lines[i];
                var s2 = lines[i + 1];
                distance += EditDistance.JaroWinkler(s1, s2);
            }
            distance /= linesCount;

            poem.Distance = distance;

            await repository.SaveAsync();
        }
    }
}
