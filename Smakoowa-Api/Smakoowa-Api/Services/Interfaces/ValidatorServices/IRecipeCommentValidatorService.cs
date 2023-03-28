namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IRecipeCommentValidatorService
    {
        public Task<ServiceResponse> ValidateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public Task<ServiceResponse> ValidateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId);
    }
}
