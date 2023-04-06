namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeLikeService
    {
        public Task<ServiceResponse> AddRecipeLike(int recipeId);
        public Task<ServiceResponse> RemoveRecipeLike(int recipeId);
    }
}
