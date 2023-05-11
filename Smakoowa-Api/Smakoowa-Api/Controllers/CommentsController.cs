namespace Smakoowa_Api.Controllers
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRecipeCommentService _recipeCommentService;
        private readonly ICommentReplyService _commentReplyService;

        public CommentsController(IRecipeCommentService recipeCommentService, ICommentReplyService commentReplyService)
        {
            _recipeCommentService = recipeCommentService;
            _commentReplyService = commentReplyService;
        }

        [HttpPost("AddRecipeComment/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            return await _recipeCommentService.AddRecipeComment(recipeCommentRequestDto, recipeId);
        }

        [HttpPost("AddCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            return await _commentReplyService.AddCommentReply(commentReplyRequestDto, commentReplyId);
        }

        [HttpPut("EditRecipeComment/{recipeCommentId}")]
        public async Task<ServiceResponse> EditRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            return await _recipeCommentService.EditRecipeComment(recipeCommentRequestDto, recipeCommentId);
        }

        [HttpPut("EditCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> EditCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            return await _commentReplyService.EditCommentReply(commentReplyRequestDto, commentReplyId);
        }

        [HttpDelete("DeleteRecipeComment/{recipeCommentId}")]
        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            return await _recipeCommentService.DeleteRecipeComment(recipeCommentId);
        }

        [HttpDelete("DeleteCommentReply/{commentReplyId}")]
        public async Task<ServiceResponse> DeleteCommentReply(int commentReplyId)
        {
            return await _commentReplyService.DeleteCommentReply(commentReplyId);
        }
    }
}
