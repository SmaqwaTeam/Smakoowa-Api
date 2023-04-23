using Smakoowa_Api.Attributes;

namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
         
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [JwtAuthorize("User", "Admin")]
        [HttpPost("AddImageToRecipe/{recipeId}")]
        public async Task<ServiceResponse> AddImageToRecipe(IFormFile image, int recipeId)
        {
            return await _imageService.AddImageToRecipe(image, recipeId);
        }

        [HttpGet("GetImage/{imageUrl}")]
        public IActionResult GetImage(string imageUrl)
        {
            var imageFileStream = _imageService.GetRecipeImage(imageUrl);
            return File(imageFileStream, "image/jpeg");
        }
    }
}
