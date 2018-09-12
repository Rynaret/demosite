using Common.Constants;
using Common.Conventions;
using Common.Models.Contexts;
using Common.Options;
using DemoSite.Hubs;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DemoSite.EventHandlers
{
    public class ReportIsReadyEventHandler : IIntegrationEventHandler<ReportIsReadyEventContext>
    {
        private readonly ReportsHub reportsHub;
        private readonly MicroServicesUrls options;

        public ReportIsReadyEventHandler(ReportsHub reportsHub, IOptions<MicroServicesUrls> options)
        {
            this.reportsHub = reportsHub;
            this.options = options.Value;
        }

        public Task Handle(ReportIsReadyEventContext @event)
        {
            return reportsHub.SendAsync($"{options.ReportService}{@event.FilePath.Replace(FolderConstants.WwwRoote, "/")}");
        }
    }
}
