using Microsoft.Extensions.Configuration;
using System.IO;

namespace Common.HostConfigurations
{
    public static class AppConfiguration
    {
        public static void SetCommonConfiguration(IConfigurationBuilder config)
        {
            var sharedFolder = Path.GetFullPath(@"../Common/");
            config.AddJsonFile(Path.Combine(sharedFolder, "appsettings.common.json"), optional: true);
        }
    }
}
