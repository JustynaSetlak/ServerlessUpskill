using System.Threading.Tasks;

namespace UpskillStore.EventPublisher.EventHandlers
{
    public interface IEventPublisher
    {
        Task PublishEvent<T>(T newEvent) where T : class, IEvent;
    }
}