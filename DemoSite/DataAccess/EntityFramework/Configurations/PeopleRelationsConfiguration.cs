using Common.DataAccess.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poems.Entities;

namespace DemoSite.DataAccess.EntityFramework.Configurations
{
    public class PeopleRelationsConfiguration : BaseEntityTypeConfiguration<People.Entities.People>
    {
        public override void Map(EntityTypeBuilder<People.Entities.People> builder)
        {
            builder.HasMany(typeof(Poem)).WithOne();
        }
    }
}
