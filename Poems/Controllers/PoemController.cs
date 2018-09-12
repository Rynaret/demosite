using Common.Conventions.Commands;
using Common.Conventions.Queries;
using Common.Models;
using Common.Models.Criterions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Poems.Entities;
using Poems.Models.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace Poems.Controllers
{
    [Route("api")]
    [ApiController]

    public class PoemController : ControllerBase
    {
        private readonly ICommandBuilder commandBuilder;
        private readonly IQueryBuilder queryBuilder;

        public PoemController(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder)
        {
            this.commandBuilder = commandBuilder;
            this.queryBuilder = queryBuilder;
        }

        [HttpPost("[action]/{personId:long}")]
        public async Task<IActionResult> GetPoem(long personId)
        {
            var createPoem = new CreatePoemContext(personId);
            await commandBuilder.ExecuteAsync(createPoem);

            var estimatePoem = new EstimatePoemContext(createPoem.IdAfterCreate);
            await commandBuilder.ExecuteAsync(estimatePoem);

            return Ok();
        }

        [EnableQuery]
        [HttpPost("[action]")]
        public async Task<IActionResult> Get([FromBody]BaseListModel<long> payload)
        {
            var getPeople = new GetCriterion();
            var result = await queryBuilder.For<Poem, IQueryable<PoemViewModel>>().WithAsync(getPeople);

            result = result.Where(x => payload.Data.Contains(x.PoetId));

            return Ok(result);
        }
    }
}
