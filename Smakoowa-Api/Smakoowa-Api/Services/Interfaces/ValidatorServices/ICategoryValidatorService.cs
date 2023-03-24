namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ICategoryValidatorService
    {
        public ServiceResponse ValidateCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto);
        public ServiceResponse ValidateEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto);
    }
}
