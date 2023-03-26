using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Mappings
{
    public class TagMapperProfile : Profile
    {
        public TagMapperProfile()
        {
            CreateMap<TagRequestDto, Tag>();
            CreateMap<Tag, GetTagResponseDto>();
        }
    }
}
