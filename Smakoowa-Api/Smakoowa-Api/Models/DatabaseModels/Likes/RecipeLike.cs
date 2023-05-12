namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class RecipeLike : Like
    {
        public virtual Recipe LikedRecipe { get; set; }
    }
}
