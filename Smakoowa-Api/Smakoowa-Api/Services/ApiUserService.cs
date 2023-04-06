namespace Smakoowa_Api.Services
{
    public class ApiUserService : IApiUserService
    {
        public int GetCurrentUserId()
        {
            return int.Parse(Program.configuration["CurrentUserId"]);
        }
    }
}
