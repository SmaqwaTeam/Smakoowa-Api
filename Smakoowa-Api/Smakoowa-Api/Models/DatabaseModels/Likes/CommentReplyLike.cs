using Smakoowa_Api.Models.DatabaseModels.Comments;

namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class CommentReplyLike : Like
    {
        public int CommentReplyId { get; set; }
        public virtual CommentReply CommentReply { get; set; }
    }
}
