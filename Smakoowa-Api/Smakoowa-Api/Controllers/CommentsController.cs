namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRecipeCommentService _recipeCommentService;

        public CommentsController(IRecipeCommentService recipeCommentService)
        {
            _recipeCommentService = recipeCommentService;
        }

        [HttpPost("AddRecipeComment/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            return await _recipeCommentService.AddRecipeComment(recipeCommentRequestDto, recipeId);
        }

        [HttpPost("AddCommentReply/{commentId}")]
        public async Task<ServiceResponse> AddCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            return await _recipeCommentService.AddCommentReply(commentReplyRequestDto, commentId);
        }

        [HttpPut("EditRecipeComment/{commentId}")]
        public async Task<ServiceResponse> EditRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int commentId)
        {
            return await _recipeCommentService.EditRecipeComment(recipeCommentRequestDto, commentId);
        }

        [HttpPut("EditCommentReply/{commentId}")]
        public async Task<ServiceResponse> EditCommentReply([FromBody] CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            return await _recipeCommentService.EditCommentReply(commentReplyRequestDto, commentId);
        }

        [HttpDelete("DeleteRecipeComment/{commentId}")]
        public async Task<ServiceResponse> DeleteRecipeComment(int commentId)
        {
            return await _recipeCommentService.DeleteRecipeComment(commentId);
        }

        [HttpDelete("DeleteCommentReply/{commentId}")]
        public async Task<ServiceResponse> DeleteCommentReply(int commentId)
        {
            return await _recipeCommentService.DeleteCommentReply(commentId);
        }
    }
}
