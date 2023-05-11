namespace Smakoowa_Api.Models.DatabaseModels.Comments
{
    public class RecipeComment : Comment
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual List<CommentReply>? CommentReplies { get; set; }
        public virtual List<RecipeCommentLike>? Likes { get; set; }
    }
}
