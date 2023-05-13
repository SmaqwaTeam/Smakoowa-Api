using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Controllers.Likes
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Likes")]
    [ApiController]
    public class RecipeLikeController : ControllerBase
    {
        private readonly IRecipeLikeService _recipeLikeService;

        public RecipeLikeController(IRecipeLikeService recipeLikeService)
        {
            _recipeLikeService = recipeLikeService;
        }

        [HttpPost("AddRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            return await _recipeLikeService.AddLike(recipeId);
        }

        [HttpDelete("RemoveRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> RemoveRecipeLike(int recipeId)
        {
            return await _recipeLikeService.RemoveLike(recipeId);
        }
    }
}
