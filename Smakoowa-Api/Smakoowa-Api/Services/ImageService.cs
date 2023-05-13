using Smakoowa_Api.Services.Interfaces.Helper;

namespace Smakoowa_Api.Services
{
    public class ImageService : IImageService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IHelperService<ImageService> _helperService;
        private readonly IImageValidatorService _imageValidatorService;
        private readonly IApiUserService _apiUserService;
        private readonly string _recipeImageUploadPath;
        private readonly string _savedImageExtension;

        public ImageService(IRecipeRepository recipeRepository, IWebHostEnvironment env, IHelperService<ImageService> helperService,
            IImageValidatorService imageValidatorService, IApiUserService apiUserService, IConfiguration configuration)
        {
            _recipeRepository = recipeRepository;
            _env = env;
            _helperService = helperService;
            _imageValidatorService = imageValidatorService;
            _apiUserService = apiUserService;
            _recipeImageUploadPath = configuration.GetSection($"FileUpload:Images:RecipeImageUploadPath").Value;
            _savedImageExtension = configuration.GetSection($"FileUpload:Images:SavedImageExtension").Value;
        }

        public FileStream GetRecipeImage(string imageId)
        {
            var imagePath = Directory.GetCurrentDirectory() + $"\\{_recipeImageUploadPath}\\" + imageId + _savedImageExtension;

            if (!File.Exists(imagePath)) throw new FileNotFoundException("Image not found.");

            return System.IO.File.OpenRead(imagePath);
        }

        public async Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId)
        {
            var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.", HttpStatusCode.NotFound);

            if (recipe.CreatorId != _apiUserService.GetCurrentUserId())
                return ServiceResponse.Error($"Recipe with id: {recipeId} doesn't belong to user.", HttpStatusCode.Unauthorized);

            var imageValidationResult = _imageValidatorService.ValidateImage(image);
            if (!imageValidationResult.SuccessStatus) return imageValidationResult;

            try
            {
                var imageId = await SaveImage(image, _recipeImageUploadPath);

                var oldImageId = recipe.ImageId;
                recipe.ImageId = imageId;

                await _recipeRepository.Edit(recipe);

                if (oldImageId != null) DeleteImage(oldImageId, _recipeImageUploadPath);

                return ServiceResponse.Success("Image uploaded.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while uploading the image.");
            }
        }

        private async Task<string> SaveImage(IFormFile image, string imageUploadPath)
        {
            var imageId = $"{Guid.NewGuid()}";
            var path = Path.Combine(_env.ContentRootPath, imageUploadPath, imageId + _savedImageExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return imageId;
        }

        private void DeleteImage(string imageId, string imageUploadPath)
        {
            var filePath = Path.Combine(_env.ContentRootPath, imageUploadPath, imageId + _savedImageExtension);

            if (File.Exists(filePath)) File.Delete(filePath);
        }
    }
}
