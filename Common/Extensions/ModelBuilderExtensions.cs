using Common.DataAccess.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var baseConfigurationType = typeof(BaseEntityTypeConfiguration<>);
            var configurationType = typeof(IEntityTypeConfiguration<>);
            var applyGenericMethod = typeof(ModelBuilder).GetMethods()
                .Where(x => x.IsPublic)
                .Where(x => x.Name == nameof(ModelBuilder.ApplyConfiguration))
                .First(x => x.GetParameters()
                    .Any(y => y.ParameterType.GetGenericTypeDefinition() == configurationType)
                );

            var applicableTypes = assembly
                .GetTypes()
                .Where(t => t.IsGenericType == false)
                .Where(t => t.BaseType != null)
                .Where(t => t.BaseType.IsGenericType)
                .Where(t => t.BaseType.GetGenericTypeDefinition() == baseConfigurationType);

            foreach (var type in applicableTypes)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface.IsConstructedGenericType && @interface.GetGenericTypeDefinition() == configurationType)
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }
    }
}
