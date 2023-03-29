namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService recipeLikeService)
        {
            _likeService = recipeLikeService;
        }

        [HttpPost("AddRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            return await _likeService.AddRecipeLike(recipeId);
        }

        [HttpPost("AddRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            return await _likeService.AddRecipeCommentLike(recipeCommentId);
        }

        [HttpPost("AddCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            return await _likeService.AddCommentReplyLike(commentReplyId);
        }

        [HttpDelete("RemoveRecipeLike/{likeId}")]
        public async Task<ServiceResponse> RemoveRecipeLike(int likeId)
        {
            return await _likeService.RemoveRecipeLike(likeId);
        }

        [HttpDelete("RemoveRecipeCommentLike/{likeId}")]
        public async Task<ServiceResponse> RemoveRecipeCommentLike(int likeId)
        {
            return await _likeService.RemoveRecipeCommentLike(likeId);
        }

        [HttpDelete("RemoveCommentReplyLike/{likeId}")]
        public async Task<ServiceResponse> RemoveCommentReplyLike(int likeId)
        {
            return await _likeService.RemoveCommentReplyLike(likeId);
        }
    }
}
