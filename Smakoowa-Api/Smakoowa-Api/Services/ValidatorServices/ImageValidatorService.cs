namespace Smakoowa_Api.Services.ValidatorServices
{
    public class ImageValidatorService : IImageValidatorService
    {
        private readonly List<string> allowedImageExtensions;
        private readonly int maxImageSizeBytes;

        public ImageValidatorService(IConfiguration configuration)
        {
            maxImageSizeBytes = int.Parse(configuration.GetSection($"Validation:Image:MaxImageSizeBytes").Value);
            allowedImageExtensions = configuration.GetSection($"Validation:Image:AllowedImageExtensions").Value.Split(", ").ToList();
        }

        public ServiceResponse ValidateImage(IFormFile image)
        {
            if(image.Length > maxImageSizeBytes)
            {
                return ServiceResponse.Error($"Image is too large, max size is {(double)maxImageSizeBytes/1000000} MB.");
            }

            if (image.ContentType.StartsWith("image/") 
                && !allowedImageExtensions.Any(a => image.ContentType.Substring("image/".Length).Contains(a)))
            {
                return ServiceResponse.Error($"Incorrect image type, allowed types: {String.Join(", ", allowedImageExtensions)}");
            }

            return ServiceResponse.Success();
        }
    }
}
