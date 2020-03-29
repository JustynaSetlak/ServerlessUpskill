namespace UpskillStore.TableStorage.Providers
{
    public interface ICloudTableClientProvider
    {
        Microsoft.WindowsAzure.Storage.Table.CloudTableClient CreateTableClient(string connectionString);
    }
}