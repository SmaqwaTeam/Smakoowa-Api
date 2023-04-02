namespace Smakoowa_Api.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginUser([FromBody] LoginUserDto model);
    }
}
