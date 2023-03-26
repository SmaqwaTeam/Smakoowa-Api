namespace Smakoowa_Api.Mappings
{
    public class TagMapperProfile : Profile
    {
        public TagMapperProfile()
        {
            CreateMap<CreateTagRequestDto, Tag>();
            CreateMap<Tag, GetTagResponseDto>();
        }
    }
}
