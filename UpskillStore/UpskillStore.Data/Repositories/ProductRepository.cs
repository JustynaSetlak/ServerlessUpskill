using System.Threading.Tasks;
using UpskillStore.Data.Dtos;
using UpskillStore.Data.Models;
using UpskillStore.Utils.Result;

namespace UpskillStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataAccessRepository<Product> _dataAccessRepository;

        public ProductRepository(IDataAccessRepository<Product> dataAccessRepository)
        {
            _dataAccessRepository = dataAccessRepository;
        }

        public async Task<DataResult<string>> CreateProduct(ProductToCreateDto productToCreate)
        {
            var product = new Product(productToCreate.Name, productToCreate.Description, productToCreate.CategoryId);

            var result = await _dataAccessRepository.CreateNewItem(product);

            return result;
        }

        public async Task GetProducts(ListAllProductsDto getProductsDto)
        {

        }
    }
}
