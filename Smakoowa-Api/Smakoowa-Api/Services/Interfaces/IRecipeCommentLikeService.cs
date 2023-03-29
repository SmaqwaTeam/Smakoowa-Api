namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeCommentLikeService
    {
        public Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId);
        public Task<ServiceResponse> RemoveRecipeCommentLike(int likeId);
    }
}
