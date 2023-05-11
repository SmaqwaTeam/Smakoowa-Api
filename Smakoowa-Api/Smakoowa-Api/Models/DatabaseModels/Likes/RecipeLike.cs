namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class RecipeLike : Like
    {
        public int RecipeId { get; set; }
        public virtual Recipe LikedRecipe { get; set; }
    }
}
