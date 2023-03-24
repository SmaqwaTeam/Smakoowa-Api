namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CategoryValidatorService : ICategoryValidatorService
    {
        private readonly IConfiguration _configuration;
        private readonly int MaxCategoryNameLength;

        public CategoryValidatorService(IConfiguration configuration)
        {
            _configuration = configuration;
            MaxCategoryNameLength = int.Parse(_configuration.GetSection("Validation:Category:MaxCategoryNameLength").Value);
        }

        public ServiceResponse ValidateCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto)
        {
            if (createCategoryRequestDto.Name.Length < 1)
            {
                return ServiceResponse.Error(Validation.CategoryNameTooShort);
            }

            if (createCategoryRequestDto.Name.Length > MaxCategoryNameLength)
            {
                return ServiceResponse.Error(Validation.CategoryNameTooLong + _configuration["MaxCategoryNameLength"]);
            }

            return ServiceResponse.Success();
        }

        public ServiceResponse ValidateEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto)
        {
            if (editCategoryRequestDto.Name.Length < 1)
            {
                return ServiceResponse.Error(Validation.CategoryNameTooShort);
            }

            if (editCategoryRequestDto.Name.Length > MaxCategoryNameLength)
            {
                return ServiceResponse.Error(Validation.CategoryNameTooLong + _configuration["MaxCategoryNameLength"]);
            }

            return ServiceResponse.Success();
        }
    }
}
