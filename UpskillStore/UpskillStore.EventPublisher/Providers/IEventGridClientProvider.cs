using Microsoft.Azure.EventGrid;

namespace UpskillStore.EventPublisher.Providers
{
    public interface IEventGridClientProvider
    {
        EventGridClient CreateEventGridClient(string key);
    }
}
