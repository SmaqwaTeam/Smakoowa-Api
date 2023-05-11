namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICommentReplyLikeService
    {
        public Task<ServiceResponse> AddCommentReplyLike(int commentReplyId);
        public Task<ServiceResponse> RemoveCommentReplyLike(int commentReplyId);
        public Task<int> GetCommentReplyLikeCount(int commentReplyId);
    }
}
