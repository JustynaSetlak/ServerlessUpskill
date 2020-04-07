using System.Threading.Tasks;
using UpskillStore.Data.Dtos;
using UpskillStore.Utils.Result;

namespace UpskillStore.Data.Repositories
{
    public interface IProductRepository
    {
        Task<DataResult<string>> CreateProduct(ProductToCreateDto productToCreate);
        Task<DataResult<ProductDto>> GetProduct(string id);
    }
}