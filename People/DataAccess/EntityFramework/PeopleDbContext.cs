using Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace People.DataAccess.EntityFramework
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
