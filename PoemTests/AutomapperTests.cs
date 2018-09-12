using Autofac;
using AutoMapper;
using Common.Autofac;
using Poems.Models.Mappings;
using Xunit;

namespace PoemTests
{
    public class AutomapperTests
    {
        [Fact]
        public void Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterMapper(typeof(PoemProfile).Assembly);
            var container = builder.Build();

            var mapper = container.Resolve<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
