using Common.Extensions;
using DemoSite.DataAccess.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using People.DataAccess.EntityFramework;
using Poems.DataAccess.EntityFramework;
using System.Linq;

namespace DemoSite.DataAccess.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromAssembly(typeof(PeopleRelationsConfiguration).Assembly);
            modelBuilder.ApplyAllConfigurationsFromAssembly(typeof(PeopleDbContext).Assembly);
            modelBuilder.ApplyAllConfigurationsFromAssembly(typeof(PoemDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
