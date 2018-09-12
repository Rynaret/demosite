using Autofac;
using AutoMapper;
using Common.Autofac;
using People.Models.Mappings;
using Xunit;

namespace PeopleTests
{
    public class AutomapperTests
    {
        [Fact]
        public void Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterMapper(typeof(PeopleProfile).Assembly);
            var container = builder.Build();

            var mapper = container.Resolve<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
