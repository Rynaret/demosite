using System.Threading.Tasks;
using Common.HostConfigurations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DemoSite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .ConfigureAppConfiguration(AppConfiguration.SetCommonConfiguration)
                .UseStartup<Startup>();
    }
}
