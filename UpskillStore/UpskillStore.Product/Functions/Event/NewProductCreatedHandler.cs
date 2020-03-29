// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;

namespace UpskillStore.Product.Functions.Event
{
    public class NewProductCreatedHandler
    {
        public NewProductCreatedHandler()
        {

        }

        [FunctionName(nameof(NewProductCreatedHandler))]
        public void Run([EventGridTrigger]EventGridEvent eventGridEvent)
        {
            var c = 1;
        }
    }
}
