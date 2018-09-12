using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Autofac;
using Common.Constants;
using Common.Conventions;
using Common.Middlewares;
using Common.Models.Contexts;
using Common.Options;
using DemoSite.DataAccess.EntityFramework;
using DemoSite.EventHandlers;
using DemoSite.Hubs;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoSite
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

            services.AddDb<ApplicationDbContext>(Configuration);
            var microServicesUrls = Configuration.GetSection(nameof(MicroServicesUrls)).Get<MicroServicesUrls>();
            services.AddHttpClient(HttpClientNames.PeopleService, client =>
            {
                client.BaseAddress = new Uri(microServicesUrls.PeopleService);
            });

            services.AddConfigurations(Configuration, assembly);
            services.AddOData();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRabbitMQ(Configuration);
            services.AddSignalR();

            var builder = new ContainerBuilder();
            builder.RegisterSignalRHubs(assembly);
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
            using (var scope = app.ApplicationServices.CreateScope())
            {
                app.ApplicationServices.GetService<ApplicationDbContext>().Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<ReportsHub>("/signalr/reports");
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.ConfigureOData(app.ApplicationServices);

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ReportIsReadyEventContext, ReportIsReadyEventHandler>();
        }
    }
}
