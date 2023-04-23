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
        private readonly string _savedImageExtansion;

        public ImageService(IRecipeRepository recipeRepository, IWebHostEnvironment env, IHelperService<ImageService> helperService,
            IImageValidatorService imageValidatorService, IApiUserService apiUserService, IConfiguration configuration)
        {
            _recipeRepository = recipeRepository;
            _env = env;
            _helperService = helperService;
            _imageValidatorService = imageValidatorService;
            _apiUserService = apiUserService;
            _recipeImageUploadPath = configuration.GetSection($"FileUpload:Images:RecipeImageUploadPath").Value;
            _savedImageExtansion = configuration.GetSection($"FileUpload:Images:SavedImageExtension").Value;
        }

        public async Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId)
        {
            var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");

            if(recipe.CreatorId != _apiUserService.GetCurrentUserId()) 
                return ServiceResponse.Error($"Recipe with id: {recipeId} doesn't belong to user.");

            var imageValidationResult = _imageValidatorService.ValidateImage(image);
            if (!imageValidationResult.SuccessStatus) return imageValidationResult;

            try
            {
                var imageUrl = await SaveImage(image, _recipeImageUploadPath);

                var oldImageId = recipe.ThumbnailImageUrl;
                recipe.ThumbnailImageUrl = imageUrl;

                await _recipeRepository.Edit(recipe);

                if (oldImageId != null) DeleteImage(oldImageId, _recipeImageUploadPath);

                return ServiceResponse.Success("Image uploaded.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while uploading the image.");
            }
        }

        public FileStream GetRecipeImage(string imageUrl)
        {
            var imagePath = Directory.GetCurrentDirectory() + $"\\{_recipeImageUploadPath}\\" + imageUrl + _savedImageExtansion;

            if (!File.Exists(imagePath)) throw new FileNotFoundException("Image not found.");

            return System.IO.File.OpenRead(imagePath);
        }

        private async Task<string> SaveImage(IFormFile image, string imageUploadPath)
        {
            var imageId = $"{Guid.NewGuid()}";
            var path = Path.Combine(_env.ContentRootPath, imageUploadPath, imageId + _savedImageExtansion);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return imageId;
        }

        private void DeleteImage(string imageId, string imageUploadPath)
        {
            var filePath = Path.Combine(_env.ContentRootPath, imageUploadPath, imageId + _savedImageExtansion);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
