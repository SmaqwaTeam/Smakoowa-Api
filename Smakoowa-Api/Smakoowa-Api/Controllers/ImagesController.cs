using Smakoowa_Api.Attributes;

namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly string _savedImageExtension;

        public ImagesController(IImageService imageService, IConfiguration configuration)
        {
            _imageService = imageService;
            _savedImageExtension = configuration.GetSection($"FileUpload:Images:SavedImageExtension").Value.Substring(1);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpPost("AddImageToRecipe/{recipeId}")]
        public async Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId)
        {
            return await _imageService.AddImageToRecipe(image, recipeId);
        }

        [HttpGet("GetRecipeImage/{imageId}")]
        public IActionResult GetRecipeImage(string imageId)
        {
            return File(_imageService.GetRecipeImage(imageId), $"image/{_savedImageExtension}");
        }
    }
}
