namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagLikeService
    {
        public Task<ServiceResponse> AddTagLike(int tagId);
        public Task<ServiceResponse> RemoveTagLike(int likeId);
    }
}
