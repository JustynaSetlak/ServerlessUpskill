using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Options;
using UpskillStore.TableStorage.Providers;
using UpskillStore.Utils.Result;
using System.Collections.Generic;
using UpskillStore.TableStorage.Extensions;
using System.Threading;

namespace UpskillStore.TableStorage.GenericRepositories
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableEntity, new()
    {
        private readonly CloudTable _table;

        public TableStorageRepository(ICloudTableClientProvider cloudTableClientProvider, IOptions<TableStorageOptions> options)
        {
            var cloudTableClient = cloudTableClientProvider.CreateTableClient(options.Value.ConnectionString);
            _table = cloudTableClient.GetTableReference(typeof(T).Name);
        }

        public async Task<DataResult<T>> CreateElementAsync(T elementToAdd)
        {
            TableOperation insertOperation = TableOperation.Insert(elementToAdd);

            var operationResult = await _table.ExecuteAsync(insertOperation);
            var result = operationResult.GetDataResult<T>();

            return result;
        }

        public async Task<DataResult<T>> GetById(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve(partitionKey, rowKey);

            var operationResult = await _table.ExecuteAsync(operation);
            var result = operationResult.GetDataResult<T>();

            return result;
        }

        public async Task<DataResult<T>> Remove(T entity)
        {
            var operation = TableOperation.Delete(entity);
            var operationResult = await _table.ExecuteAsync(operation);
            var result = operationResult.GetDataResult<T>();

            return result;
        }

        public async Task<DataResult<T>> Update(T entity)
        {
            var operation = TableOperation.Replace(entity);
            var operationResult = await _table.ExecuteAsync(operation);
            var result = operationResult.GetDataResult<T>();

            return result;
        }

        public async Task<List<T>> GetElementByPropertyAsync(string propertyName, string value)
        {            
            var rangeQuery = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition(propertyName, QueryComparisons.Equal, value));
             
            var result = await _table.ExecuteQueryAsync(rangeQuery);
            return result;
        }

        public async Task<List<T>> QueryDataAsync(TableQuery<T> tableQuery, CancellationToken cancellationToken = default)
        {
            var result = await _table.ExecuteQueryAsync(tableQuery, cancellationToken);

            return result;
        }
    }
}
