namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeCommentLikeService
    {
        public Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId);
        public Task<ServiceResponse> RemoveRecipeCommentLike(int recipeCommentId);
        public Task<int> GetRecipeCommentLikeCount(int recipeCommentId);
    }
}
