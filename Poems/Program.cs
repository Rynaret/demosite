using System.Threading.Tasks;
using Common.Constants;
using Common.HostConfigurations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Poems
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AppConfiguration.SetCommonConfiguration)
                .UseStartup<Startup>();
    }
}
