namespace Smakoowa_Api.Services.Interfaces.Likes
{
    public interface ITagLikeService : ILikeService
    {
        public Task<IEnumerable<TagLike>> GetUserTagLikes();
    }
}
