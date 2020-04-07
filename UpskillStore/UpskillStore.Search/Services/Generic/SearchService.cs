using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpskillStore.Search.Models;
using UpskillStore.Search.Options;
using UpskillStore.Search.Providers;

namespace UpskillStore.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly Lazy<ISearchIndexClient> _searchIndex;

        public SearchService(ISearchIndexClientProvider searchIndexClientProvider, IOptions<SearchOptions> searchOptions)
        {
            _searchIndex = new Lazy<ISearchIndexClient>(
                () => searchIndexClientProvider.Create(searchOptions.Value.Name, searchOptions.Value.Key, searchOptions.Value.IndexName));
        }

        public async Task MergeOrUpload(ISearchable item)
        {
            var itemsToUpload = new List<ISearchable> { item };

            var batch = IndexBatch.MergeOrUpload(itemsToUpload);

            try
            {
                await _searchIndex.Value.Documents.IndexAsync(batch);
            }
            catch (IndexBatchException e)
            {
                //log e
            }
        }

        public async Task<List<T>> Search<T>(string searchText, SearchParameters searchParameters) where T : class, ISearchable
        {
            var searchResult = await _searchIndex.Value.Documents.SearchAsync<T>(searchText, searchParameters);

            var resultList = searchResult.Results.Select(x => x.Document).ToList();

            return resultList;
        }
    }
}
