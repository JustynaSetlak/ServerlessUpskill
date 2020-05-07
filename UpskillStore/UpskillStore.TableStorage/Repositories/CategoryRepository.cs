using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Dtos;
using UpskillStore.TableStorage.GenericRepositories;
using UpskillStore.TableStorage.Models;
using UpskillStore.TableStorage.Repositories.Interfaces;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string PARTITION_KEY = nameof(ProductCategory);

        private readonly ITableStorageRepository<ProductCategory> _tableStorageRepository;

        public CategoryRepository(ITableStorageRepository<ProductCategory> tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task<DataResult<List<CategoryDto>>> GetByPropertyValue(string propertyName, string value)
        {
            var opertationResult = await _tableStorageRepository.GetElementByPropertyAsync(propertyName, value);

            var data = opertationResult.Select(x => new CategoryDto(x.Name, x.Description)).ToList();

            return new SuccessfulDataResult<List<CategoryDto>>(data);
        }

        public async Task<Result> CheckIfExists(string id)
        {
            var operationResult = await _tableStorageRepository.GetById(PARTITION_KEY, id);

            var result = new Result(operationResult.IsSuccessful);

            return result;
        }

        public async Task<DataResult<CategoryDto>> GetById(string id)
        {
            var operationResult = await _tableStorageRepository.GetById(PARTITION_KEY, id);

            var retrievedData = operationResult.IsSuccessful
                ? new CategoryDto(operationResult.Value.Name, operationResult.Value.Description)
                : null;

            var result = new DataResult<CategoryDto>(operationResult.IsSuccessful, retrievedData);

            return result;
        }

        public async Task<DataResult<string>> CreateCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            var category = new ProductCategory(categoryToAddDto.Name, categoryToAddDto.Description);

            var operationResult = await _tableStorageRepository.CreateElementAsync(category);

            var result = new DataResult<string>(operationResult.IsSuccessful, operationResult.Value?.RowKey);
            return result;
        }
    }
}
