using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.GenericRepositories
{
    public interface ITableStorageRepository<T> where T : TableEntity, new()
    {
        Task<DataResult<T>> CreateElementAsync(T elementToAdd);
        Task<DataResult<T>> GetById(string partitionKey, string rowKey);
        Task<List<T>> GetElementAsync(string propertyName, string value);
    }
}
