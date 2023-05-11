namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IRecipeCommentValidatorService
    {
        public Task<ServiceResponse> ValidateCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public Task<ServiceResponse> ValidateEditRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, RecipeComment editedRecipeComment);
    }
}
