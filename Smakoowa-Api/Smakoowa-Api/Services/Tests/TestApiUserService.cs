namespace Smakoowa_Api.Services.Tests
{
    public class TestApiUserService : IApiUserService
    {
        public int GetCurrentUserId()
        {
            return 1;
        }

        public bool CurrentUserIsAdmin()
        {
            return false;
        }
    }
}
