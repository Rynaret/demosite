using Common.Conventions.Queries;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using People.Models.Criterions;
using People.Models.ExternalJsonModels;
using People.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace People.Queries
{
    public class GetRandomUserQuery : IQuery<GetRandomUserCriterion, RandomUserResultModel>
    {
        private readonly ExternalApiOptions options;
        private readonly IHttpClientFactory httpClientFactory;

        public GetRandomUserQuery(IOptions<ExternalApiOptions> options, IHttpClientFactory httpClientFactory)
        {
            this.options = options.Value;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RandomUserResultModel> AskAsync(GetRandomUserCriterion criterion)
        {
            var response = await httpClientFactory.CreateClient(options.RandomUserUrl).GetAsync("");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RandomUserResultModel>(content);
            return result;
        }
    }
}
