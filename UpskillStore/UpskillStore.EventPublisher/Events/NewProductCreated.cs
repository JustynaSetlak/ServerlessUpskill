using UpskillStore.EventPublisher.EventHandlers;

namespace UpskillStore.EventPublisher.Events
{
    public class NewProductCreated : IEvent
    {
        public NewProductCreated(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
