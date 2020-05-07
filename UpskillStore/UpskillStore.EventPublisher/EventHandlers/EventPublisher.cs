using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.EventPublisher.Options;
using UpskillStore.EventPublisher.Providers;

namespace UpskillStore.EventPublisher.EventHandlers
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Lazy<IEventGridClient> _eventGridClient;
        private readonly EventGridOptions _eventGridOptions;

        public EventPublisher(IEventGridClientProvider eventGridClientProvider, IOptions<EventGridOptions> options)
        {
            _eventGridClient = new Lazy<IEventGridClient>(
                    () => eventGridClientProvider.CreateEventGridClient(options.Value.Key)
                );

            _eventGridOptions = options.Value;
        }

        public async Task PublishEvent<T>(T newEvent) where T : class, IEvent
        {
            var eventTypeName = newEvent.GetType().Name;

            var events = new List<EventGridEvent>
            {
                new EventGridEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = eventTypeName,
                    Data = newEvent,
                    Subject = eventTypeName,
                    DataVersion = "1.0",
                }
            };

            var topicHostname = new Uri(_eventGridOptions.Endpoint).Host;

            await _eventGridClient.Value.PublishEventsAsync(topicHostname, events);
        }
    }
}
