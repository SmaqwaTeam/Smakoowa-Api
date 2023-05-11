namespace Smakoowa_Api.Services.Interfaces
{
    public interface IApiUserGetterService
    {
        public Task<ServiceResponse> GetUserById(int userId);
    }
}
