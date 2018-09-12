using Common.Constants;
using Common.Conventions.Queries;
using Common.Extensions;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryBuilder queryBuilder;
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(IQueryBuilder queryBuilder, IHttpClientFactory httpClientFactory)
        {
            this.queryBuilder = queryBuilder;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var result = await httpClientFactory.CreateClient(HttpClientNames.PeopleService)
                .GetAsync("api/Get")
                .ToModel<List<PeopleViewModel>>();

            return View(result);
        }
    }
}