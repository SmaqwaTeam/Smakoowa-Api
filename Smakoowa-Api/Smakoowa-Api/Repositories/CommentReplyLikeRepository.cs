namespace Smakoowa_Api.Repositories
{
    public class CommentReplyLikeRepository : BaseRepository<CommentReplyLike>, ICommentReplyLikeRepository
    {
        public CommentReplyLikeRepository(DataContext context) : base(context) { }
    }
}
