﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Dtos;
using UpskillStore.TableStorage.GenericRepositories;
using UpskillStore.TableStorage.Models;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string PARTITION_KEY = nameof(Category);

        private readonly ITableStorageRepository<Category> _tableStorageRepository;

        public CategoryRepository(ITableStorageRepository<Category> tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task<DataResult<List<CategoryDto>>> GetByPropertyValue(string propertyName, string value)
        {
            var opertationResult = await _tableStorageRepository.GetElementAsync(propertyName, value);

            var data = opertationResult.Select(x => new CategoryDto(x.Name, x.Description)).ToList();

            return new SuccessfulDataResult<List<CategoryDto>>(data);
        }

        public async Task<Result> CheckIfExists(string id)
        {
            var operationResult = await _tableStorageRepository.GetById(PARTITION_KEY, id);

            var result = new Result(operationResult.IsSuccessful);

            return result;
        }

        public async Task<DataResult<string>> CreateCategoryAsync(CategoryToAddDto categoryToAddDto)
        {
            var category = new Category(categoryToAddDto.Name, categoryToAddDto.Description);

            var operationResult = await _tableStorageRepository.CreateElementAsync(category);

            var result = new DataResult<string>(operationResult.IsSuccessful, operationResult.Value?.RowKey);
            return result;
        }
    }
}