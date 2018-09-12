using Common.DataAccess.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poems.Entities;

namespace Poems.DataAccess.EntityFramework.Configurations
{
    public class PoemConfiguration : BaseEntityTypeConfiguration<Poem>
    {
        public override void Map(EntityTypeBuilder<Poem> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(512);
        }
    }
}
