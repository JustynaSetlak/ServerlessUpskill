using Microsoft.Azure.Search;

namespace UpskillStore.Search.Providers
{
    public class SearchIndexClientProvider : ISearchIndexClientProvider
    {
        public ISearchIndexClient Create(string searchServiceName, string key, string indexName)
        {   
            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(key));
            return serviceClient.Indexes.GetClient(indexName);
        }
    }
}
