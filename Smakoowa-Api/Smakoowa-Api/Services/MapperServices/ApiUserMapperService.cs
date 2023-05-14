namespace Smakoowa_Api.Services.MapperServices
{
    public class ApiUserMapperService : IApiUserMapperService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public ApiUserMapperService(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiUserResponseDto> MapGetApiUserResponseDto(ApiUser user)
        {
            var mappedUser = _mapper.Map<ApiUserResponseDto>(user);
            mappedUser.UserRoles = (await _userManager.GetRolesAsync(user)).ToList();
            return mappedUser;
        }

        public LoginResponse MapUserLoginResponse(ApiUser user, string token)
        {
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Token = token;
            loginResponse.User = _mapper.Map<ApiUserResponseDto>(user);
            return loginResponse;
        }

        public ApiUser MapUserRegisterRequest(RegisterRequest user)
        {
            return _mapper.Map<ApiUser>(user);
        }
    }
}
