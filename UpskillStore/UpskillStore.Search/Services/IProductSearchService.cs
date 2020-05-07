using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.Search.Dtos;

namespace UpskillStore.Search.Services
{
    public interface IProductSearchService
    {
        Task MergeOrUpload(ProductSearchDto productSearchDto);

        Task<List<ProductSearchDto>> Search(ProductSearchParamsDto productSearchParams);

        Task<ProductSearchDto> GetById(string id);
    }
}