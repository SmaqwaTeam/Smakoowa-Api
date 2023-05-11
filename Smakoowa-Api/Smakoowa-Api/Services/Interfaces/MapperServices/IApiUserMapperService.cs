namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IApiUserMapperService
    {
        public Task<ApiUserResponseDto> MapGetApiUserResponseDto(ApiUser user);
    }
}
