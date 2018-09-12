using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using System;
using System.Linq;

namespace Common.Middlewares
{
    public static class ODataMiddleware
    {
        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            // @note Using default asp core routing system (not OData routing system)
            // Use EnableQuery attribute

            return builder.GetEdmModel();
        }

        public static void ConfigureOData(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var defaultConventions = ODataRoutingConventions.CreateDefault();
            // restricts access to /$metadata url
            var conventions = defaultConventions.Except(
                defaultConventions.OfType<MetadataRoutingConvention>());

            // uri resolver
            var uriResolver = new UnqualifiedODataUriResolver
            {
                EnableCaseInsensitive = true
            };

            app.UseMvc(routeBuilder =>
            {
                // allows to use EnableQuery on non-OData routing
                routeBuilder.EnableDependencyInjection();

                // available operations
                routeBuilder.Filter().Expand().Select().OrderBy().MaxTop(100).Count();

                routeBuilder.MapODataServiceRoute("odata", "api",
                    builder =>
                    {
                        builder.AddService(ServiceLifetime.Singleton, _ => GetEdmModel(serviceProvider));
                        builder.AddService(ServiceLifetime.Singleton, _ => conventions);
                        builder.AddService<ODataUriResolver>(ServiceLifetime.Singleton, _ => uriResolver);
                    });
            });
        }
    }
}
