using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using UpskillStore.TableStorage.Options;

namespace UpskillStore.TableStorage.Providers
{
    public class CloudTableClientProvider : ICloudTableClientProvider
    {
        public CloudTableClient CreateTableClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            return tableClient;
        }

    }
}
