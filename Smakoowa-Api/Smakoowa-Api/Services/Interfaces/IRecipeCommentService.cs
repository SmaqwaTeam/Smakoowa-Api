namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeCommentService
    {
        public Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId);
        public Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId);
    }
}
