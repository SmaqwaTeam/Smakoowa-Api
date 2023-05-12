namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class RecipeCommentLike : Like
    {
        public virtual RecipeComment LikedRecipeComment { get; set; }
    }
}
