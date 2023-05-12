namespace Smakoowa_Api.Services.MapperServices
{
    public class CommentReplyMapperService : CommentMapperService<CommentReply>, ICommentReplyMapperService
    {
        public CommentReplyMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
