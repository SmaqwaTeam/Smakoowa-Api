namespace Smakoowa_Api.Models.DatabaseModels
{
    public class CommentReply : Updatable, IDbKey, ILikeable
    {
        public int Id { get; set; }
        public string ReplyContent { get; set; }

        public int RepliedCommentId { get; set; }
        public virtual RecipeComment RepliedComment { get; set; }

        public virtual List<CommentReplyLike>? Likes { get; set; }
    }
}
