using Common.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Common.DataAccess.EntityFramework.Configurations
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            BaseMap(builder);
            Map(builder);
        }

        public abstract void Map(EntityTypeBuilder<T> builder);

        public virtual void BaseMap(EntityTypeBuilder<T> builder)
        {
            var isEntity = typeof(T)
                .GetInterfaces()
                .Contains(typeof(IHasKey<long>));
            if (isEntity)
            {
                builder.Property(x => ((IHasKey<long>)x).Id).ValueGeneratedOnAdd();
            }
        }
    }
}
