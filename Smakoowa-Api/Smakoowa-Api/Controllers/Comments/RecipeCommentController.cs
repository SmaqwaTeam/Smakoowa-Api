using Smakoowa_Api.Services.Interfaces.Comments;

namespace Smakoowa_Api.Controllers.Comments
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Comments")]
    [ApiController]
    public class RecipeCommentController : ControllerBase
    {
        private readonly IRecipeCommentService _recipeCommentService;

        public RecipeCommentController(IRecipeCommentService recipeCommentService)
        {
            _recipeCommentService = recipeCommentService;
        }

        [HttpPost("AddRecipeComment/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            return await _recipeCommentService.AddComment(recipeCommentRequestDto, recipeId);
        }

        [HttpPut("EditRecipeComment/{recipeCommentId}")]
        public async Task<ServiceResponse> EditRecipeComment([FromBody] RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            return await _recipeCommentService.EditComment(recipeCommentRequestDto, recipeCommentId);
        }

        [HttpDelete("DeleteRecipeComment/{recipeCommentId}")]
        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            return await _recipeCommentService.DeleteComment(recipeCommentId);
        }
    }
}
