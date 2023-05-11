namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagLikeService
    {
        public Task<ServiceResponse> AddTagLike(int tagId);
        public Task<ServiceResponse> RemoveTagLike(int tagId);
        public Task<IEnumerable<TagLike>> GetUserTagLikes();
        public Task<int> GetTagLikeCount(int tagId);
    }
}
