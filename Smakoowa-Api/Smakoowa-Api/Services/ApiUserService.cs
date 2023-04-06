namespace Smakoowa_Api.Services
{
    public class ApiUserService : IApiUserService
    {
        //private readonly UserManager<ApiUser> _userManager;

        //public ApiUserService(UserManager<ApiUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public int GetCurrentUserId()
        {
            return int.Parse(Program.configuration["CurrentUserId"]);
        }

        //public async Task<List<TagLike>> GetCurrentUserLikedTags()
        //{
        //    return (await _userManager.FindByIdAsync(GetCurrentUserId().ToString())).TagLikes;
        //}
    }
}
