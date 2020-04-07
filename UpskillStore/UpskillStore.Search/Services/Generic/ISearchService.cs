using Microsoft.Azure.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.Search.Models;

namespace UpskillStore.Search.Services
{
    public interface ISearchService
    {
        Task MergeOrUpload(ISearchable item);

        Task<List<T>> Search<T>(string searchText, SearchParameters searchParameters) where T : class, ISearchable;
    }
}