namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ICommentReplyMapperService
    {
        public CommentReply MapCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId);
        public CommentReply MapEditCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, CommentReply editedCommentReply);
    }
}
