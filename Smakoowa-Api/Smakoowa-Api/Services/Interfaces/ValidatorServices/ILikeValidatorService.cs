namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ILikeValidatorService
    {
        public Task<ServiceResponse> ValidateRecipeLike(int recipeId);
        public Task<ServiceResponse> ValidateRecipeCommentLike(int recipeCommentId);
        public Task<ServiceResponse> ValidateCommentReplyLike(int commentReplyId);
    }
}
