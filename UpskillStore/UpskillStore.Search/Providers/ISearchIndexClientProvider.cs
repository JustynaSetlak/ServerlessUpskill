using Microsoft.Azure.Search;

namespace UpskillStore.Search.Providers
{
    public interface ISearchIndexClientProvider
    {
        ISearchIndexClient Create(string searchServiceName, string key, string indexName);
    }
}