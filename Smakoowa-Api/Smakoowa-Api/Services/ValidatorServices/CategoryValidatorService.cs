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

        public async Task<ServiceResponse> ValidateCategoryRequestDto(CategoryRequestDto categoryRequestDto)
        {
            var validationResponse = ValidateNameLength(categoryRequestDto.Name, "Category");

            if (!validationResponse.SuccessStatus)
            {
                return validationResponse;
            }

            if (await _categoryRepository.CheckIfExists(c => c.Name == categoryRequestDto.Name))
            {
                return ServiceResponse.Error($"A category with name {categoryRequestDto.Name} already exists.");
            }

            return ServiceResponse.Success();
        }
    }
}