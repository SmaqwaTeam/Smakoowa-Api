namespace Smakoowa_Api.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginUser(LoginRequest model);
        public Task<ServiceResponse> RegisterUser(RegisterRequest model);
    }
}
