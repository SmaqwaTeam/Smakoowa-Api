namespace Smakoowa_Api.Services.Interfaces
{
    public interface IApiUserService
    {
        public Task<int?> GetCurrentUserId();
    }
}
