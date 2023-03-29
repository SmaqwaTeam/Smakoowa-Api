namespace Smakoowa_Api.Services.Interfaces
{
    public interface ILikeService
    {
        public Task<ServiceResponse> AddRecipeLike(int recipeId);
        public Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId);
        public Task<ServiceResponse> AddCommentReplyLike(int commentReplyId);
        public Task<ServiceResponse> RemoveRecipeLike(int likeId);
        public Task<ServiceResponse> RemoveRecipeCommentLike(int likeId);
        public Task<ServiceResponse> RemoveCommentReplyLike(int likeId);
    }
}
