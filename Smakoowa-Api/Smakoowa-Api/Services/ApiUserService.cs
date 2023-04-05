namespace Smakoowa_Api.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApiUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetCurrentUserId()
        {
            return int.Parse(Program.configuration["CurrentUserId"]);
        }
    }
}
