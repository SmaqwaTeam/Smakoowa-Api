namespace Smakoowa_Api.Services.Interfaces.Likes
{
    public interface ILikeService
    {
        public Task<ServiceResponse> AddLike(int likedId);
        public Task<ServiceResponse> RemoveLike(int likedId);
        public Task<int> GetLikeCount(int likedId);
    }
}
