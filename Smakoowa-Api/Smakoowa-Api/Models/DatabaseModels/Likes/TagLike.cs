namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class TagLike : Like
    {
        public virtual Tag LikedTag { get; set; }
    }
}
