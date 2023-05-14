namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IApiUserMapperService
    {
        public Task<ApiUserResponseDto> MapGetApiUserResponseDto(ApiUser user);
        public LoginResponse MapUserLoginResponse(ApiUser user, string token);
        public ApiUser MapUserRegisterRequest(RegisterRequest user);
    }
}
