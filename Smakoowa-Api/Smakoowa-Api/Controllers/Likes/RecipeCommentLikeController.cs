using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Controllers.Likes
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Likes")]
    [ApiController]
    public class RecipeCommentLikeController : ControllerBase
    {
        private readonly IRecipeCommentLikeService _recipeCommentLikeService;

        public RecipeCommentLikeController(IRecipeCommentLikeService recipeCommentLikeService)
        {
            _recipeCommentLikeService = recipeCommentLikeService;
        }

        [HttpPost("AddRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            return await _recipeCommentLikeService.AddLike(recipeCommentId);
        }

        [HttpDelete("RemoveRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> RemoveRecipeCommentLike(int recipeCommentId)
        {
            return await _recipeCommentLikeService.RemoveLike(recipeCommentId);
        }
    }
}
