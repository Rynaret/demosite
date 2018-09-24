using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Constants;
using Common.Conventions;
using Common.Conventions.Queries;
using Common.Extensions;
using Common.Models;
using Common.Models.Contexts;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IQueryBuilder queryBuilder;
        private readonly IEventBus eventBus;

        public MainController(IHttpClientFactory httpClientFactory, IQueryBuilder queryBuilder, IEventBus eventBus)
        {
            this.httpClientFactory = httpClientFactory;
            this.queryBuilder = queryBuilder;
            this.eventBus = eventBus;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetInfo()
        {
            await httpClientFactory.CreateClient(HttpClientNames.PeopleService).PostAsync("api/CreatePerson", null);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetInfoMQ()
        {
            var @event = new CreatePersonEventContext();
            eventBus.Publish(@event);
            return Ok();
        }

        [EnableQuery]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var result = await httpClientFactory.CreateClient(HttpClientNames.PeopleService)
                .GetAsync("api/Get")
                .ToModel<List<PeopleViewModel>>();

            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult GenerateReport()
        {
            var @event = new GenerateReportEventContext();
            eventBus.Publish(@event);
            return Ok();
        }
    }
}