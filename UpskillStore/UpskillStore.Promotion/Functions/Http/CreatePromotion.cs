using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using UpskillStore.Promotion.HttpRequests;
using UpskillStore.Common.Constants;
using System;
using UpskillStore.TableStorage.Repositories.Interfaces;
using UpskillStore.Utils.Wrappers;
using AutoMapper;
using UpskillStore.TableStorage.Dtos.Promotion;
using FluentValidation;
using UpskillStore.Promotion.Services;

namespace UpskillStore.Promotion.Functions.Http
{
    public class CreatePromotion
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePromotionHttpRequest> _validator;
        private readonly IPromotionEventPublishService _promotionEventPublishService;

        public CreatePromotion(
            IPromotionRepository promotionRepository, 
            IGuidGenerator guidGenerator, 
            IMapper mapper, 
            IValidator<CreatePromotionHttpRequest> validator,
            IPromotionEventPublishService promotionEventPublishService)
        {
            _promotionRepository = promotionRepository;
            _guidGenerator = guidGenerator;
            _mapper = mapper;
            _validator = validator;
            _promotionEventPublishService = promotionEventPublishService;
        }

        [FunctionName(nameof(CreatePromotion))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.POST)] CreatePromotionHttpRequest createPromotionHttpRequest)
        {
            var validationResult = await _validator.ValidateAsync(createPromotionHttpRequest);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var createPromotionCommand = _mapper.Map<CreateNewPromotionCommand>(createPromotionHttpRequest);
            createPromotionCommand.Id = _guidGenerator.GenerateGuid().ToString();

            createPromotionCommand.IsActive = createPromotionCommand.StartDate <= DateTime.UtcNow.Date
                && (!createPromotionCommand.EndDate.HasValue
                    || (createPromotionCommand.EndDate.HasValue && createPromotionCommand.EndDate >= DateTime.UtcNow.Date));

            var result = await _promotionRepository.Create(createPromotionCommand);

            if (!result.IsSuccessful)
            {
                return new BadRequestResult();
            }

            if (createPromotionCommand.IsActive)
            {
                await _promotionEventPublishService.PublishNewPromotionAppliedEvent(createPromotionCommand.Id, createPromotionCommand.PromotionCategoryName);
            }

            return new NoContentResult();
        }
    }
}
