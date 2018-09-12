using Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Poems.DataAccess.EntityFramework
{
    public class PoemDbContext : DbContext
    {
        public PoemDbContext(DbContextOptions<PoemDbContext> options)
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
