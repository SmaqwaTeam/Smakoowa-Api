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

        [HttpPut("EditRecipeComment/{commentId}")]
        public async Task<ServiceResponse> EditRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int commentId)
        {
            return await _recipeCommentService.EditRecipeComment(recipeCommentRequestDto, commentId);
        }

        [HttpDelete("DeleteRecipeComment/{commentId}")]
        public async Task<ServiceResponse> DeleteRecipeComment(int commentId)
        {
            return await _recipeCommentService.DeleteRecipeComment(commentId);
        }
    }
}
