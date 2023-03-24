namespace Smakoowa_Api.Repositories
{
    public class CommentReplyRepository : BaseRepository<CommentReply>, ICommentReplyRepository
    {
        public CommentReplyRepository(DataContext context) : base(context) { }
    }
}
