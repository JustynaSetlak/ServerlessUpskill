using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpskillStore.Data.Extensions;
using UpskillStore.Promotion.HttpRequests;
using UpskillStore.Promotion.Services;
using UpskillStore.Promotion.Validators;
using UpskillStore.TableStorage.Extensions;
using UpskillStore.Utils.Extensions;

[assembly: FunctionsStartup(typeof(UpskillStore.Promotion.Startup))]
namespace UpskillStore.Promotion
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddAppSettingsToConfiguration();

            builder.RegisterEventServices();
            builder.AddEventOptions();

            builder.AddDataAccessOptions();
            builder.RegisterDataAccessServices();

            builder.AddTableStorageOptions();
            builder.RegisterTableStorageServices();

            builder.RegisterUtilServices();

            builder.Services.AddTransient<IPromotionEventPublishService, PromotionEventPublishService>();
            builder.Services.AddTransient<IValidator<CreatePromotionHttpRequest>, CreatePromotionHttpRequestValidator>();
        }
    }
}
