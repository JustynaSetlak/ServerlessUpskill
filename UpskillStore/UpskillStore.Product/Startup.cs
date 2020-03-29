using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpskillStore.Data.Extensions;
using UpskillStore.Product.HttpRequests;
using UpskillStore.Product.Validators;
using UpskillStore.TableStorage.Extensions;
using UpskillStore.Utils.Extensions;

[assembly: FunctionsStartup(typeof(UpskillStore.Product.Startup))]
namespace UpskillStore.Product
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddAppSettingsToConfiguration();

            builder.AddDataAccessOptions();
            builder.RegisterDataAccessServices();

            builder.AddTableStorageOptions();
            builder.RegisterTableStorageServices();

            builder.Services.AddTransient<IValidator<CreateProductHttpRequest>, CreateProductHttpRequestValidator>();
        }
    }
}
