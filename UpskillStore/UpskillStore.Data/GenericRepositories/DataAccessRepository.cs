using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;
using UpskillStore.Data.Options;
using UpskillStore.Utils.Result;

namespace UpskillStore.Data.Repositories
{
    public class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
    {
        private readonly IDocumentClient _documentClient;
        private readonly DataAccessOptions _dataAccessOptions;
        private readonly Uri _documentCollectionUri;

        public DataAccessRepository(IDocumentClient documentClient, IOptions<DataAccessOptions> dataAccessOptions)
        {
            _documentClient = documentClient;
            _dataAccessOptions = dataAccessOptions.Value;

            _documentCollectionUri = UriFactory.CreateDocumentCollectionUri(
                dataAccessOptions.Value.DatabaseName, 
                dataAccessOptions.Value.CollectionName);
        }

        public async Task<DataResult<string>> CreateNewItem(T item)
        {
            var response = await _documentClient.CreateDocumentAsync(_documentCollectionUri, item);

            var isSuccessful = response.StatusCode == HttpStatusCode.Created;

            return new DataResult<string>(isSuccessful, response.ActivityId);
        }

        public async Task<DataResult<T>> Get(string id)
        {
            var documentUri = UriFactory.CreateDocumentUri(_dataAccessOptions.DatabaseName, _dataAccessOptions.CollectionName, id);
            var response = await _documentClient.ReadDocumentAsync<T>(documentUri);

            var isSuccessful = true;
            return new DataResult<T>(isSuccessful, response.Document);
        }
    }
}
