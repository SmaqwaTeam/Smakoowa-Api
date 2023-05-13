using Smakoowa_Api.Services.Interfaces.Comments;
using Smakoowa_Api.Services.Interfaces.Helper;

namespace Smakoowa_Api.Services.Comments
{
    public class CommentReplyService : CommentService<CommentReply>, ICommentReplyService
    {
        public CommentReplyService(ICommentReplyMapperService commentMapperService, ICommentReplyValidatorService commentValidatorService,
            IBaseRepository<CommentReply> commentRepository, IApiUserService apiUserService,
            IHelperService<CommentService<CommentReply>> helperService)
            : base(commentMapperService, commentValidatorService, commentRepository, apiUserService, helperService)
        {
        }
    }
}
