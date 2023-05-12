namespace Smakoowa_Api.Services.MapperServices
{
    public class CommentReplyMapperService : CommentMapperService, ICommentReplyMapperService
    {
        public CommentReplyMapperService(IMapper mapper) : base(mapper)
        {
        }

        public CommentReply MapCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            var mappedCommentReply = _mapper.Map<CommentReply>(commentReplyRequestDto);
            mappedCommentReply.CommentedId = commentId;
            return mappedCommentReply;
        }

        public CommentReply MapEditCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, CommentReply editedCommentReply)
        {
            return (CommentReply)MapEditCommentRequestDto(commentReplyRequestDto, editedCommentReply);
        }
    }
}
