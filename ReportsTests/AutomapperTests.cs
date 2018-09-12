using Autofac;
using AutoMapper;
using Common.Autofac;
using Reports.Models.Mappings;
using Xunit;

namespace ReportsTests
{
    public class AutomapperTests
    {
        [Fact]
        public void Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterMapper(typeof(PeopleReportProfile).Assembly);
            var container = builder.Build();

            var mapper = container.Resolve<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
