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
