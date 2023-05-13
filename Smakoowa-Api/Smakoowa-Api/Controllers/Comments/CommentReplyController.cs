using Smakoowa_Api.Services.Interfaces.Comments;

namespace Smakoowa_Api.Controllers.Comments
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Comments")]
    [ApiController]
    public class CommentReplyController : ControllerBase
    {
        private readonly ICommentReplyService _commentReplyService;

        public CommentReplyController(ICommentReplyService commentReplyService)
        {
            _commentReplyService = commentReplyService;
        }

        [HttpPost("AddCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            return await _commentReplyService.AddComment(commentReplyRequestDto, commentReplyId);
        }

        [HttpPut("EditCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> EditCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            return await _commentReplyService.EditComment(commentReplyRequestDto, commentReplyId);
        }

        [HttpDelete("DeleteCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> DeleteCommentReply(int commentReplyId)
        {
            return await _commentReplyService.DeleteComment(commentReplyId);
        }
    }
}
