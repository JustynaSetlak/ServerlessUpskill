using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UpskillStore.EventPublisher.EventHandlers;
using UpskillStore.EventPublisher.Options;
using UpskillStore.EventPublisher.Providers;
using EventsPublisher = UpskillStore.EventPublisher.EventHandlers.EventPublisher;

namespace UpskillStore.Data.Extensions
{
    public static class FunctionsHostBuilderExtensions
    {
        public static void RegisterEventServices(this IFunctionsHostBuilder builder)
        {
            builder.Services.TryAddTransient<IEventPublisher, EventsPublisher>();
            builder.Services.TryAddTransient<IEventGridClientProvider, EventGridClientProvider>();
        }

        public static void AddEventOptions(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<EventGridOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(EventGridOptions)).Bind(settings);
                });
        }
    }
}
