using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace UpskillStore.EventPublisher.Providers
{
    public class EventGridClientProvider : IEventGridClientProvider
    {
        public EventGridClient CreateEventGridClient(string key)
        {
            var topicCredentials = new TopicCredentials(key);
            var client = new EventGridClient(topicCredentials);

            return client;
        }
    }
}
