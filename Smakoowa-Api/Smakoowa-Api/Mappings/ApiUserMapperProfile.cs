namespace Smakoowa_Api.Mappings
{
    public class ApiUserMapperProfile : Profile
    {
        public ApiUserMapperProfile()
        {
            CreateMap<ApiUser, ApiUserResponseDto>();
            CreateMap<RegisterRequest, ApiUser>();
        }
    }
}
