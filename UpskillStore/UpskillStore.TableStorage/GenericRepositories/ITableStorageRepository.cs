using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.GenericRepositories
{
    public interface ITableStorageRepository<T> where T : TableEntity, new()
    {
        Task<DataResult<T>> CreateElementAsync(T elementToAdd);
        Task<DataResult<T>> GetById(string partitionKey, string rowKey);
        Task<List<T>> GetElementByPropertyAsync(string propertyName, string value);
        Task<List<T>> QueryDataAsync(TableQuery<T> tableQuery, CancellationToken cancellationToken = default);
        Task<DataResult<T>> Remove(T entity);
        Task<DataResult<T>> Update(T entity);
    }
}
