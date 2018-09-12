using AutoMapper;
using Common.Constants;
using Common.Conventions;
using Common.Conventions.Commands;
using Common.Extensions;
using Common.Models;
using Common.Models.Contexts;
using Common.Models.Reports;
using Reports.Models.Contexts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reports.EventHandlers
{
    public class GenerateReportEventHandler : IIntegrationEventHandler<GenerateReportEventContext>
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICommandBuilder commandBuilder;
        private readonly IMapper mapper;
        private readonly IEventBus eventBus;

        public GenerateReportEventHandler(IHttpClientFactory httpClientFactory, ICommandBuilder commandBuilder, IMapper mapper, IEventBus eventBus)
        {
            this.httpClientFactory = httpClientFactory;
            this.commandBuilder = commandBuilder;
            this.mapper = mapper;
            this.eventBus = eventBus;
        }

        public async Task Handle(GenerateReportEventContext @event)
        {
            var data = await httpClientFactory.CreateClient(HttpClientNames.PeopleService)
                .GetAsync("api/Get")
                .ToModel<List<PeopleViewModel>>();

            var dataForReport = mapper.Map<List<PeopleReport>>(data);

            var getReport = new GenerateExcelReportContext(dataForReport, typeof(PeopleReport).GetMetadata());
            await commandBuilder.ExecuteAsync(getReport);

            var reportIsReady = new ReportIsReadyEventContext(getReport.GeneratedFilePath);
            eventBus.Publish(reportIsReady);
        }
    }
}
