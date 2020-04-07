using System.Threading.Tasks;
using UpskillStore.Search.Dtos;

namespace UpskillStore.Search.Services
{
    public interface IProductSearchService
    {
        Task MergeOrUpload(ProductSearchDto productSearchDto);
        Task<System.Collections.Generic.List<ProductSearchDto>> Search(ProductSearchParamsDto productSearchParams);
    }
}