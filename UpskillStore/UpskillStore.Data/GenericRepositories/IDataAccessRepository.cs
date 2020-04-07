using System.Threading.Tasks;
using UpskillStore.Utils.Result;

namespace UpskillStore.Data.Repositories
{
    public interface IDataAccessRepository<T> where T : class
    {
        Task<DataResult<string>> CreateNewItem(T item);

        Task<DataResult<T>> Get(string id);
    }
}