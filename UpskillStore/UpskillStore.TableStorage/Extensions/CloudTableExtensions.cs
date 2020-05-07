using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UpskillStore.TableStorage.Extensions
{
    public static class CloudTableExtensions
    {
        public static async Task<List<T>> ExecuteQueryAsync<T>(this CloudTable table, TableQuery<T> query,
            CancellationToken ct = default) where T : TableEntity, new()
        {
            var items = new List<T>();
            TableContinuationToken token = null;

            do
            {
                var seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);

            } while (token != null && !ct.IsCancellationRequested);

            return items;
        }
    }
}
