namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class CommentReplyLike : Like
    {
        public virtual CommentReply LikedCommentReply { get; set; }
    }
}
