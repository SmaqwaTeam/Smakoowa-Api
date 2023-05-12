namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class RecipeCommentLike : Like
    {
        //public int LikedId { get; set; }
        public virtual RecipeComment LikedRecipeComment { get; set; }
    }
}
