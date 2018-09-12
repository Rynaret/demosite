using Common.Conventions.Queries;
using Microsoft.Extensions.Options;
using People.Models.Criterions;
using People.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace People.Queries
{
    public class GetRandomQuoteQuery : IQuery<GetRandomQuoteCriterion, string>
    {
        private readonly ExternalApiOptions options;
        private readonly IHttpClientFactory httpClientFactory;

        public GetRandomQuoteQuery(IOptions<ExternalApiOptions> options, IHttpClientFactory httpClientFactory)
        {
            this.options = options.Value;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> AskAsync(GetRandomQuoteCriterion criterion)
        {
            var response = await httpClientFactory.CreateClient(options.QuoteUrl).GetAsync("");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
