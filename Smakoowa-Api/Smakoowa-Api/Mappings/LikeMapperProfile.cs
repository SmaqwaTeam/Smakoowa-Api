namespace Smakoowa_Api.Mappings
{
    public class LikeMapperProfile : Profile
    {
        public LikeMapperProfile()
        {
            CreateMap<Like, LikeResponseDto>();
        }
    }
}
