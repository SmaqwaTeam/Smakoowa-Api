namespace Smakoowa_Api.Controllers.Likes
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Likes")]
    [ApiController]
    public class CommentReplyLikeController : ControllerBase
    {
        private readonly ICommentReplyLikeService _commentReplyLikeService;

        public CommentReplyLikeController(ICommentReplyLikeService commentReplyLikeService)
        {
            _commentReplyLikeService = commentReplyLikeService;
        }

        [HttpPost("AddCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            return await _commentReplyLikeService.AddLike(commentReplyId);
        }

        [HttpDelete("RemoveCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> RemoveCommentReplyLike(int commentReplyId)
        {
            return await _commentReplyLikeService.RemoveLike(commentReplyId);
        }
    }
}
