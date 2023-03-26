using Smakoowa_Api.Models.RequestDtos.Category;

namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CategoryValidatorService : ICategoryValidatorService
    {
        private readonly IConfiguration _configuration;
        private readonly ICategoryRepository _categoryRepository;
        private readonly int MaxCategoryNameLength;
        private readonly int MinCategoryNameLength;

        public CategoryValidatorService(IConfiguration configuration, ICategoryRepository categoryRepository)
        {
            _configuration = configuration;
            MaxCategoryNameLength = int.Parse(_configuration.GetSection("Validation:Category:MaxCategoryNameLength").Value);
            MinCategoryNameLength = int.Parse(_configuration.GetSection("Validation:Category:MinCategoryNameLength").Value);
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse> ValidateCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto)
        {
            if (createCategoryRequestDto.Name.Length < MinCategoryNameLength)
            {
                return ServiceResponse.Error($"Category name must have a minimum of {MinCategoryNameLength} characters.");
            }

            if (createCategoryRequestDto.Name.Length > MaxCategoryNameLength)
            {
                return ServiceResponse.Error($"Category name must have a maximum of {MaxCategoryNameLength} characters.");
            }

            if(await _categoryRepository.CheckIfExists(c => c.Name == createCategoryRequestDto.Name))
            {
                return ServiceResponse.Error($"A category with name {createCategoryRequestDto.Name} already exists.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto)
        {
            if (editCategoryRequestDto.Name.Length < MinCategoryNameLength)
            {
                return ServiceResponse.Error($"Category name must have a minimum of {MinCategoryNameLength} characters.");
            }

            if (editCategoryRequestDto.Name.Length > MaxCategoryNameLength)
            {
                return ServiceResponse.Error($"Category name must have a maximum of {MaxCategoryNameLength} characters.");
            }

            if (await _categoryRepository.CheckIfExists(c => c.Name == editCategoryRequestDto.Name))
            {
                return ServiceResponse.Error($"A category with name {editCategoryRequestDto.Name} already exists.");
            }

            return ServiceResponse.Success();
        }
    }
}
