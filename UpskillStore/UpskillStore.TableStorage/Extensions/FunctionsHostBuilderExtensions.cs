using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UpskillStore.TableStorage.GenericRepositories;
using UpskillStore.TableStorage.Options;
using UpskillStore.TableStorage.Providers;
using UpskillStore.TableStorage.Repositories;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.TableStorage.Extensions
{
    public static class FunctionsHostBuilderExtensions
    {
        public static void RegisterTableStorageServices(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient(typeof(ITableStorageRepository<>), typeof(TableStorageRepository<>));
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IPromotionRepository, PromotionRepository>();
            builder.Services.TryAddTransient<ICloudTableClientProvider, CloudTableClientProvider>();
        }

        public static void AddTableStorageOptions(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<TableStorageOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(TableStorageOptions)).Bind(settings);
                });
        }
    }
}
