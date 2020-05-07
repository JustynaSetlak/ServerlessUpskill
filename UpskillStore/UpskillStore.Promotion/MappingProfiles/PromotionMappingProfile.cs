using AutoMapper;
using UpskillStore.Promotion.HttpRequests;
using UpskillStore.TableStorage.Dtos.Promotion;

namespace UpskillStore.Promotion.MappingProfiles
{
    public class PromotionMappingProfile : Profile
    {
        public PromotionMappingProfile()
        {
            CreateMap<CreatePromotionHttpRequest, CreateNewPromotionCommand>();
        }
    }
}
