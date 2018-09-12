using Common.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DemoSite.Hubs
{
    public class ReportsHub : Hub
    {
        private readonly IHubContext<ReportsHub> hubContext;

        public ReportsHub(IHubContext<ReportsHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task SendAsync(string path)
        {
            return hubContext.Clients.All.SendAsync("Send", path);
        }
    }
}
