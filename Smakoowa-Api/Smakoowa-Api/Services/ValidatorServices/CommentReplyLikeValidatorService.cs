namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CommentReplyLikeValidatorService : LikeValidatorService<CommentReplyLike, CommentReply>, ICommentReplyLikeValidatorService
    {
        public CommentReplyLikeValidatorService(IBaseRepository<CommentReply> likedItemRepository, IBaseRepository<CommentReplyLike> likeRepository,
            IApiUserService apiUserService)
            : base(likedItemRepository, likeRepository, apiUserService)
        {
        }
    }
}
