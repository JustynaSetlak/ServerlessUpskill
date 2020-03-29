using System;
using System.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UpskillStore.Utils.Extensions
{
    public static class FunctionsHostBuilderExtensions
    {
        public static IFunctionsHostBuilder AddAppSettingsToConfiguration(this IFunctionsHostBuilder builder)
        {
            var isDevelopment = IsDevelopmentEnvironment();

            var configurationBuilder = new ConfigurationBuilder();

            if (isDevelopment)
            {
                configurationBuilder
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }

            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration));

            if (descriptor?.ImplementationInstance is IConfiguration configRoot)
            {
                configurationBuilder.AddConfiguration(configRoot);
            }

            var configuration = configurationBuilder
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), configuration));

            return builder;
        }

        public static bool IsDevelopmentEnvironment()
        {
            const string developmentEnvironment = "Development";
            const string azureFunctionsEnvironmentVariableName = "AZURE_FUNCTIONS_ENVIRONMENT";

            return developmentEnvironment.Equals(Environment.GetEnvironmentVariable(azureFunctionsEnvironmentVariableName), StringComparison.OrdinalIgnoreCase);
        }
    }
}
