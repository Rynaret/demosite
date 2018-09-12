using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Autofac;
using Common.Constants;
using Common.Conventions;
using Common.Middlewares;
using Common.Models.Contexts;
using Common.Options;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.DataAccess.EntityFramework;
using People.EventHandlers;
using People.Options;

namespace People
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var assembly = GetType().Assembly;

            services.AddDb<PeopleDbContext>(Configuration);
            services.AddConfigurations(Configuration, assembly);

            var externalApiOptions = Configuration.GetSection(nameof(ExternalApiOptions)).Get<ExternalApiOptions>();
            services.AddHttpClient(externalApiOptions.QuoteUrl, client =>
            {
                client.BaseAddress = new Uri(externalApiOptions.QuoteUrl);
            });
            services.AddHttpClient(externalApiOptions.RandomUserUrl, client =>
            {
                client.BaseAddress = new Uri(externalApiOptions.RandomUserUrl);
            });
            var microServicesUrls = Configuration.GetSection(nameof(MicroServicesUrls)).Get<MicroServicesUrls>();
            services.AddHttpClient(HttpClientNames.PoemService, client =>
            {
                client.BaseAddress = new Uri(microServicesUrls.PoemService);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRabbitMQ(Configuration);
            services.AddOData();

            var builder = new ContainerBuilder();
            builder.RegisterCQDependencies(assembly);
            builder.RegisterEventHandlers(assembly);
            builder.RegisterMapper(assembly);
            builder.Populate(services);
            var container = builder.Build();
            IServiceProvider provider = new AutofacServiceProvider(container);
            return provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.ConfigureOData(app.ApplicationServices);

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PeopleGetInfoEventContext, PeopleGetInfoEventHandler>();
        }
    }
}
