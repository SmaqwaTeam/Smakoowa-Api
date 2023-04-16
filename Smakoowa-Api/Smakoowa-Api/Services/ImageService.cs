using Smakoowa_Api.Models.DatabaseModels;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Services.Interfaces;

namespace Smakoowa_Api.Services
{
    public class ImageService : IImageService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IHelperService<ImageService> _helperService;

        public ImageService(IRecipeRepository recipeRepository, IWebHostEnvironment env, IHelperService<ImageService> helperService)
        {
            _recipeRepository = recipeRepository;
            _env = env;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId)
        {
            var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");

            //validate image
            //validate recipe belongs to current user

            try
            {
                var imageUrl = await SaveImage(image);
                recipe.ThumbnailImageUrl = imageUrl;

                await _recipeRepository.Edit(recipe);
                return ServiceResponse.Success("Image uploaded.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while uploading the image.");
            }

        }

        public FileStream GetImage(string imageUrl)
        {
            string dir = Directory.GetCurrentDirectory();
            var imagePath = dir + imageUrl;//= Path.Combine(dir, imageUrl);
            return System.IO.File.OpenRead(imagePath);
            //return this.File(imageFileStream, "image/jpeg");
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(_env.ContentRootPath, "Images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"~/images/{fileName}";
        }
    }
}
