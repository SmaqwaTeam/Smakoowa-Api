namespace Smakoowa_Api.Models.Identity
{
    public class ApiUser : IdentityUser<int>, IDbModel
    {
        public virtual List<Recipe> Recipes { get; set; }
        public virtual List<RecipeComment> RecipeComments { get; set; }
        public virtual List<CommentReply> CommentReplies { get; set; }
        public virtual List<RecipeLike> RecipeLikes { get; set; }
        public virtual List<RecipeCommentLike> RecipeCommentLikes { get; set; }
        public virtual List<CommentReplyLike> CommentReplyLikes { get; set; }
        public virtual List<TagLike> TagLikes { get; set; }
    }
}
