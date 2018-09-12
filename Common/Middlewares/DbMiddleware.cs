using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Middlewares
{
    public static class DbMiddleware
    {
        public static void AddDb<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DemoSiteDbConnection")));
            services.AddScoped(typeof(DbContext), typeof(T));
        }
    }
}
