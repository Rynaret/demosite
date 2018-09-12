using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Constants;
using Common.Conventions.Commands;
using Common.Conventions.Queries;
using Common.Extensions;
using Common.Models;
using Common.Models.Criterions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using People.Models.Contexts;

namespace People.Controllers
{
    [Route("api")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ICommandBuilder commandBuilder;
        private readonly IQueryBuilder queryBuilder;
        private readonly IHttpClientFactory httpClientFactory;

        public PeopleController(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder, IHttpClientFactory httpClientFactory)
        {
            this.commandBuilder = commandBuilder;
            this.queryBuilder = queryBuilder;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetInfo()
        {
            var createUser = new CreateUserContext();
            await commandBuilder.ExecuteAsync(createUser);

            var setQuote = new SetQuoteContext(createUser.IdAfterCreate);
            await commandBuilder.ExecuteAsync(setQuote);

            await httpClientFactory.CreateClient(HttpClientNames.PoemService)
                .PostAsync($"api/GetPoem/{createUser.IdAfterCreate}", null);

            return Ok();
        }

        [EnableQuery]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var getPeople = new GetCriterion();
            var result = await queryBuilder.For<Entities.People, IQueryable<PeopleViewModel>>().WithAsync(getPeople);

            var data = await result.ToListAsync();
            var peopleIds = data.Select(x => x.Id).ToList();

            if (peopleIds.Any())
            {
                var payload = JsonConvert.SerializeObject(new BaseListModel<long> { Data = peopleIds });
                var poems = await httpClientFactory.CreateClient(HttpClientNames.PoemService)
                    .PostAsync("api/Get", new StringContent(payload, Encoding.UTF8, "application/json"))
                    .ToModel<List<PoemViewModel>>();
                foreach (var item in data)
                {
                    var poem = poems.Where(x => x.PoetId == item.Id).FirstOrDefault();
                    item.Poem = poem;
                }
            }

            return Ok(data);
        }
    }
}