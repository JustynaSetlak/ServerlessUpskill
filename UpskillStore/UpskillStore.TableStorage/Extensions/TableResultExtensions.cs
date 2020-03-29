using Microsoft.WindowsAzure.Storage.Table;
using UpskillStore.Utils.Extensions;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Extensions
{
    public static class TableResultExtensions
    {
        public static DataResult<T> GetDataResult<T>(this TableResult operationResult) where T : TableEntity, new()
        {
            var isResultSuccessful = operationResult.HttpStatusCode.IsHttpStatusCodeSucessful();

            var result = isResultSuccessful
                ? (DataResult<T>)new SuccessfulDataResult<T>(operationResult.Result as T)
                : new ErrorDataResult<T>(operationResult.Result as T);

            return result;
        }
    }
}
