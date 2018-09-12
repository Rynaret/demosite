using Common.DataAccess.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace People.DataAccess.EntityFramework.Configurations
{
    public class PeopleConfiguration : BaseEntityTypeConfiguration<Entities.People>
    {
        public override void Map(EntityTypeBuilder<Entities.People> builder)
        {
            builder.Property(x => x.City).HasMaxLength(64);
            builder.Property(x => x.Email).HasMaxLength(254);
            builder.Property(x => x.FirstName).HasMaxLength(64);
            builder.Property(x => x.LastName).HasMaxLength(64);
            builder.Property(x => x.PictureMedium).HasMaxLength(2083);
            builder.Property(x => x.Quote).HasMaxLength(1024);
            builder.Property(x => x.Street).HasMaxLength(256);
        }
    }
}
