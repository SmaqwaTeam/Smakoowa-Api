namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CategoryValidatorService : BaseValidatorService, ICategoryValidatorService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryValidatorService(IConfiguration configuration, ICategoryRepository categoryRepository)
        : base(configuration, "Validation:Category")
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> ValidateCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto)
        {
            var validationResponse = ValidateNameLength(createCategoryRequestDto.Name, "Category");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _categoryRepository.CheckIfExists(c => c.Name == createCategoryRequestDto.Name))
            {
                return ServiceResponse.Error($"A category with name {createCategoryRequestDto.Name} already exists.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto)
        {
            var validationResponse = ValidateNameLength(editCategoryRequestDto.Name, "Category");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _categoryRepository.CheckIfExists(c => c.Name == editCategoryRequestDto.Name))
            {
                return ServiceResponse.Error($"A category with name {editCategoryRequestDto.Name} already exists.");
            }

            return ServiceResponse.Success();
        }
    }
}