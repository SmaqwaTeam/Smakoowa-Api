namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class TagLike : Like
    {
        public int TagId { get; set; }
        public Tag LikedTag { get; set; }
    }
}
