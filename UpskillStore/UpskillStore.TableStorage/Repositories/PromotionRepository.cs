using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Dtos.Promotion;
using UpskillStore.TableStorage.GenericRepositories;
using UpskillStore.TableStorage.Models;
using UpskillStore.TableStorage.Repositories.Interfaces;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly IMapper _mapper;
        private readonly ITableStorageRepository<Promotion> _tableStorageRepository;

        public PromotionRepository(IMapper mapper, ITableStorageRepository<Promotion> tableStorageRepository)
        {
            _mapper = mapper;
            _tableStorageRepository = tableStorageRepository;
        }

        public async Task<DataResult<CreateNewPromotionCommandResult>> Create(CreateNewPromotionCommand createNewPromotionCommand)
        {
            var entity = _mapper.Map<Promotion>(createNewPromotionCommand);

            entity.RowKey = createNewPromotionCommand.Id;
            entity.PartitionKey = createNewPromotionCommand.PromotionCategoryName;

            var result = await _tableStorageRepository.CreateElementAsync(entity);

            if (!result.IsSuccessful)
            {
                return new ErrorDataResult<CreateNewPromotionCommandResult>();
            }

            var dataResult = new CreateNewPromotionCommandResult(entity.RowKey, entity.PartitionKey);

            return new SuccessfulDataResult<CreateNewPromotionCommandResult>(dataResult);
        }

        public async Task<DataResult<PromotionDto>> GetById(string id, string partitionKey)
        {
            var operationResult = await _tableStorageRepository.GetById(partitionKey, id);

            if (!operationResult.IsSuccessful)
            {
                return new ErrorDataResult<PromotionDto>();
            }

            var promotion = _mapper.Map<PromotionDto>(operationResult.Value);
            return new SuccessfulDataResult<PromotionDto>(promotion);
        }

        public async Task<Result> ToggleActivePromotion(string id, string partitionKey)
        {
            var operationResult = await _tableStorageRepository.GetById(partitionKey, id);

            if (!operationResult.IsSuccessful)
            {
                return new ErrorResult();
            }

            var promotionToUpdate = operationResult.Value;
            promotionToUpdate.IsActive = !promotionToUpdate.IsActive;

            var result = await _tableStorageRepository.Update(promotionToUpdate);

            return result.IsSuccessful ? (Result)new ErrorResult() : new SuccessResult();
        }

        public async Task<Result> Remove(string id, string partitionKey)
        {
            var operationResult = await _tableStorageRepository.GetById(partitionKey, id);

            if (!operationResult.IsSuccessful)
            {
                return new ErrorResult();
            }

            var promotion = operationResult.Value;
            promotion.IsActive = false;
            promotion.IsDeleted = true;

            var result = await _tableStorageRepository.Update(promotion);

            return result.IsSuccessful ? (Result) new ErrorResult() : new SuccessResult();
        }

        public async Task<List<PromotionDto>> GetPromotions()
        {
            var query = new TableQuery<Promotion>()
                .Where(TableQuery.GenerateFilterConditionForBool(nameof(Promotion.IsDeleted), QueryComparisons.Equal, false));

            var promotionList = await _tableStorageRepository.QueryDataAsync(query);

            var result = _mapper.Map<List<PromotionDto>>(promotionList);

            return result;
        }

        public async Task<List<PromotionDto>> GetActivePromotionByElementId(string promotionCategoryName, string elementId, bool onlyForVip)
        {
            var notDeletedFilter = TableQuery.GenerateFilterConditionForBool(nameof(Promotion.IsDeleted), QueryComparisons.Equal, false);
            var activeFilter = TableQuery.GenerateFilterConditionForBool(nameof(Promotion.IsActive), QueryComparisons.Equal, true);
            var promotionCategoryFilter = TableQuery.GenerateFilterCondition(nameof(Promotion.PartitionKey), QueryComparisons.Equal, promotionCategoryName);
            var specificElementIdFilter = TableQuery.GenerateFilterCondition(nameof(Promotion.ElementId), QueryComparisons.Equal, elementId);

            var predicate = TableQuery.CombineFilters(
                        TableQuery.CombineFilters(
                            TableQuery.CombineFilters(notDeletedFilter, TableOperators.And, specificElementIdFilter),
                            TableOperators.And,
                            activeFilter),
                        TableOperators.And, promotionCategoryFilter);

            if (!onlyForVip)
            {
                var onlyForVipFilter = TableQuery.GenerateFilterConditionForBool(nameof(Promotion.OnlyForVip), QueryComparisons.Equal, false);
                predicate = TableQuery.CombineFilters(predicate, TableOperators.And, onlyForVipFilter);
            }

            var query = new TableQuery<Promotion>().Where(predicate);

            var promotionList = await _tableStorageRepository.QueryDataAsync(query);

            var result = _mapper.Map<List<PromotionDto>>(promotionList);

            return result;
        }
    }
}
