using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Common.Middlewares
{
    public static class FileServerMiddleware
    {
        public static void SetFolder(this IApplicationBuilder app, string reportsStorage)
        {
            var reportsStoragePath = Path.Combine(Directory.GetCurrentDirectory(), reportsStorage);

            if (Directory.Exists(reportsStoragePath) == false)
            {
                Directory.CreateDirectory(reportsStoragePath);
            }

            app.UseFileServer(
                new FileServerOptions
                {
                    EnableDirectoryBrowsing = true,
                    FileProvider = new PhysicalFileProvider(reportsStoragePath),
                    //RequestPath = new PathString("/avatars"),
                    EnableDefaultFiles = false
                }
            );
        }
    }
}
