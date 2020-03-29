using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using UpskillStore.Data.Options;
using UpskillStore.Data.Repositories;

namespace UpskillStore.Data.Extensions
{
    public static class FunctionsHostBuilderExtensions
    {
        public static void RegisterDataAccessServices(this IFunctionsHostBuilder builder)
        {
            builder.Services.TryAddTransient<IProductRepository, ProductRepository>();
            builder.Services.TryAddTransient(typeof(IDataAccessRepository<>), typeof(DataAccessRepository<>));

            //TO DO
            builder.Services.TryAddSingleton<IDocumentClient>(new DocumentClient(new Uri("https://localhost:8081"), "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="));
        }

        public static void AddDataAccessOptions(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<DataAccessOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(DataAccessOptions)).Bind(settings);
                });
        }
    }
}
