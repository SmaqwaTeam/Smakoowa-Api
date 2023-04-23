namespace Smakoowa_Api.Services.Interfaces
{
    public interface IImageService
    {
        public Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId);
        public FileStream GetRecipeImage(string imageId);
    }
}
