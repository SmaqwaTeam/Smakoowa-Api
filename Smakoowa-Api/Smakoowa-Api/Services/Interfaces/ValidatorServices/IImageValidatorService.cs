namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IImageValidatorService
    {
        public ServiceResponse ValidateImage(IFormFile image);
    }
}
