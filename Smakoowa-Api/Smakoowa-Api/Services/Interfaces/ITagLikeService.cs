namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagLikeService : ILikeService
    {
        public Task<IEnumerable<TagLike>> GetUserTagLikes();
    }
}
