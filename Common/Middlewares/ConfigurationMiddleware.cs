using Common.Autofac;
using Common.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Common.Middlewares
{
    public static class ConfigurationMiddleware
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var optionsType = typeof(IConfigurableOptions);

            var assemblies = new Assembly[] { typeof(AutofacConfig).Assembly, assembly };

            var types = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => optionsType.IsAssignableFrom(x))
                .ToList();

            foreach (var type in types)
            {
                var extensions = typeof(OptionsConfigurationServiceCollectionExtensions);
                var method = extensions.GetMethods()
                    .Where(x => x.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure))
                    .Where(x => x.IsGenericMethod)
                    .Where(x => x.GetParameters().Count() == 2)
                    .FirstOrDefault()
                    .MakeGenericMethod(type);

                method.Invoke(services, new object[] { services, configuration.GetSection(type.Name) });
            }
        }
    }
}
