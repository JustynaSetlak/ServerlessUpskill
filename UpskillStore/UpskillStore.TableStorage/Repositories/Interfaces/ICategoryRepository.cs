using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Dtos;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<DataResult<string>> CreateCategoryAsync(CategoryToAddDto categoryToAddDto);

        Task<DataResult<List<CategoryDto>>> GetByPropertyValue(string propertyName, string value);

        Task<Result> CheckIfExists(string id);

        Task<DataResult<CategoryDto>> GetById(string id);
    }
}
