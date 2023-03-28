using Smakoowa_Api.Models.DatabaseModels.Comments;

namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class RecipeCommentLike : Like
    {
        public int RecipeCommentId { get; set; }
        public virtual RecipeComment RecipeComment { get; set; }
    }
}
