namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public Task<ServiceResponse> AddCommentReply(CommentReplyRequestDto commentReplyRequestDto, int recipeCommentId);
        public Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId);
        public Task<ServiceResponse> EditCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentReplyId);
        public Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId);
        public Task<ServiceResponse> DeleteCommentReply(int commentReplyId);
    }
}
