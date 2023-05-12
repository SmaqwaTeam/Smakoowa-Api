namespace Smakoowa_Api.Models.Interfaces
{
    public interface ILike : IDbModel, ICreatable
    {
        public int LikedId { get; set; }
    }
}
