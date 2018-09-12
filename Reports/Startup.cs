using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Autofac;
using Common.Constants;
using Common.Conventions;
using Common.Middlewares;
using Common.Models.Contexts;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reports.EventHandlers;
using System;
using System.IO;

namespace Reports
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

            var microServicesUrls = Configuration.GetSection(nameof(MicroServicesUrls)).Get<MicroServicesUrls>();
            services.AddHttpClient(HttpClientNames.PeopleService, client =>
            {
                client.BaseAddress = new Uri(microServicesUrls.PeopleService);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRabbitMQ(Configuration);

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

            var reportsStoragePath = Path.Combine(Directory.GetCurrentDirectory(), FolderConstants.ReportsStorage);

            if (!Directory.Exists(reportsStoragePath))
                Directory.CreateDirectory(reportsStoragePath);

            app.UseStaticFiles();
            app.SetFolder(FolderConstants.ReportsStorage);

            app.UseMvc();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<GenerateReportEventContext, GenerateReportEventHandler>();
        }
    }
}
