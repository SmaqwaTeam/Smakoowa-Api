namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class TagLike : Like
    {
        public int TagId { get; set; }
        public virtual Tag LikedTag { get; set; }
    }
}
