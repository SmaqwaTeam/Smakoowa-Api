namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICommentReplyService
    {
        public Task<ServiceResponse> AddCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentId);
        public Task<ServiceResponse> EditCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentReplyId);
        public Task<ServiceResponse> DeleteCommentReply(int commentReplyId);
    }
}
