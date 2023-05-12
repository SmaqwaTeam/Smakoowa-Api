namespace Smakoowa_Api.Services
{
    public class CommentReplyLikeService : LikeService<CommentReplyLike>, ICommentReplyLikeService
    {
        public CommentReplyLikeService(IBaseRepository<CommentReplyLike> likeRepository, IHelperService<LikeService<CommentReplyLike>> helperService,
            IApiUserService apiUserService, ICommentReplyLikeValidatorService likeValidatorService) 
            : base(likeRepository, helperService, apiUserService, likeValidatorService, LikeableType.CommentReply)
        {
        }
    }
}
