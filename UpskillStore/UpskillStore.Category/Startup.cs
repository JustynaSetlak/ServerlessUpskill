using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpskillStore.Category.HttpRequests;
using UpskillStore.Category.Validators;
using UpskillStore.TableStorage.Extensions;
using UpskillStore.Utils.Extensions;

[assembly: FunctionsStartup(typeof(UpskillStore.Category.Startup))]
namespace UpskillStore.Category
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddAppSettingsToConfiguration();

            builder.AddTableStorageOptions();
            builder.RegisterTableStorageServices();

            builder.Services.AddTransient<IValidator<CreateCategoryHttpRequest>, CreateCategoryHttpRequestValidator>();

        }
    }
}
