using Smakoowa_Api.Attributes;

namespace Smakoowa_Api.Controllers
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IRecipeLikeService _recipeLikeService;
        private readonly IRecipeCommentLikeService _recipeCommentLikeService;
        private readonly ICommentReplyLikeService _commentReplyLikeService;

        public LikesController(ICommentReplyLikeService commentReplyLikeService, IRecipeCommentLikeService recipeCommentLikeService, 
            IRecipeLikeService recipeLikeService)
        {
            _commentReplyLikeService = commentReplyLikeService;
            _recipeCommentLikeService = recipeCommentLikeService;
            _recipeLikeService = recipeLikeService;
        }

        [HttpPost("AddRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            return await _recipeLikeService.AddRecipeLike(recipeId);
        }

        [HttpPost("AddRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            return await _recipeCommentLikeService.AddRecipeCommentLike(recipeCommentId);
        }

        [HttpPost("AddCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            return await _commentReplyLikeService.AddCommentReplyLike(commentReplyId);
        }

        [HttpDelete("RemoveRecipeLike/{likeId}")]
        public async Task<ServiceResponse> RemoveRecipeLike(int likeId)
        {
            return await _recipeLikeService.RemoveRecipeLike(likeId);
        }

        [HttpDelete("RemoveRecipeCommentLike/{likeId}")]
        public async Task<ServiceResponse> RemoveRecipeCommentLike(int likeId)
        {
            return await _recipeCommentLikeService.RemoveRecipeCommentLike(likeId);
        }

        [HttpDelete("RemoveCommentReplyLike/{likeId}")]
        public async Task<ServiceResponse> RemoveCommentReplyLike(int likeId)
        {
            return await _commentReplyLikeService.RemoveCommentReplyLike(likeId);
        }
    }
}
