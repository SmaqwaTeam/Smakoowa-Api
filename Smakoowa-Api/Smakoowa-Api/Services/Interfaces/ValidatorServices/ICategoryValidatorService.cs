namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ICategoryValidatorService
    {
        public Task<ServiceResponse> ValidateCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto);
        public Task<ServiceResponse> ValidateEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto);
    }
}
