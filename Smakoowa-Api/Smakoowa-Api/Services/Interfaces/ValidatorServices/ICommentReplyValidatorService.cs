namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ICommentReplyValidatorService
    {
        public Task<ServiceResponse> ValidateEditCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, CommentReply editedCommentReply);
        public Task<ServiceResponse> ValidateCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId);
    }
}
