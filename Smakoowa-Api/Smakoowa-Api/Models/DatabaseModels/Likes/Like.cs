namespace Smakoowa_Api.Models.DatabaseModels.Likes
{
    public class Like : Creatable, ILike
    {
        public int Id { get; set; }
        public LikeableType LikeableType { get; set; }
    }
}
