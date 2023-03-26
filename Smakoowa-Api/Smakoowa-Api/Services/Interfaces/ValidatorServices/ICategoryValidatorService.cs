namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ICategoryValidatorService
    {
        public Task<ServiceResponse> ValidateCategoryRequestDto(CategoryRequestDto categoryRequestDto);
    }
}
