using System.Threading.Tasks;

namespace Common.Conventions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
