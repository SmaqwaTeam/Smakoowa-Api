namespace Smakoowa_Api.Services.ValidatorServices
{
    public class ImageValidatorService : IImageValidatorService
    {
        private readonly List<string> _allowedImageExtensions;
        private readonly int _maxImageSizeBytes;

        public ImageValidatorService(IConfiguration configuration)
        {
            _maxImageSizeBytes = int.Parse(configuration.GetSection($"Validation:Image:MaxImageSizeBytes").Value);
            _allowedImageExtensions = configuration.GetSection($"Validation:Image:AllowedImageExtensions").Value.Split(", ").ToList();
        }

        public ServiceResponse ValidateImage(IFormFile image)
        {
            if (image.Length > _maxImageSizeBytes)
            {
                return ServiceResponse.Error($"Image is too large, max size is {(double)_maxImageSizeBytes / 1000000} MB.",
                    HttpStatusCode.RequestEntityTooLarge);
            }

            if (image.ContentType.StartsWith("image/")
                && !_allowedImageExtensions.Any(a => image.ContentType.Substring("image/".Length).Contains(a)))
            {
                return ServiceResponse.Error($"Incorrect image type, allowed types: {String.Join(", ", _allowedImageExtensions)}",
                    HttpStatusCode.UnsupportedMediaType);
            }

            return ServiceResponse.Success();
        }
    }
}
