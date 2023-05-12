namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class TagLike : Like
    {
        //public int LikedId { get; set; }
        public virtual Tag LikedTag { get; set; }
    }
}
