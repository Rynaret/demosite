using Common.Conventions.Queries;
using Common.Extensions;
using Microsoft.Extensions.Options;
using Poems.Models.Criterions;
using Poems.Models.ExternalJsonModels;
using Poems.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poems.Queries
{
    public class GetRandomPoemQuery : IQuery<GetRandomPoemCriterion, RandomPoemResultModel[]>
    {
        private readonly ExternalApiOptions options;
        private readonly IHttpClientFactory httpClientFactory;

        public GetRandomPoemQuery(IOptions<ExternalApiOptions> options, IHttpClientFactory httpClientFactory)
        {
            this.options = options.Value;
            this.httpClientFactory = httpClientFactory;
        }

        public Task<RandomPoemResultModel[]> AskAsync(GetRandomPoemCriterion criterion)
        {
            return httpClientFactory.CreateClient(options.RandomPoemUrl)
                .GetAsync("")
                .ToModel<RandomPoemResultModel[]>();
        }
    }
}
