namespace Smakoowa_Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapperService _categoryMapperService;
        private readonly ICategoryValidatorService _categoryValidatorService;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryMapperService categoryMapperService, 
            ICategoryValidatorService categoryValidatorService)
        {
            _categoryRepository = categoryRepository;
            _categoryMapperService = categoryMapperService;
            _categoryValidatorService = categoryValidatorService;
        }

        public async Task<ServiceResponse> Create(CreateCategoryRequestDto createCategoryRequestDto)
        {
            var validationResult = _categoryValidatorService.ValidateCreateCategoryRequestDto(createCategoryRequestDto);
            if(!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var category = _categoryMapperService.MapCreateCategoryRequestDto(createCategoryRequestDto);
            var createdCategory = await _categoryRepository.Create(category);

            if (createdCategory == null) return ServiceResponse.Error(string.Format(Validation.FailedToCreateCategory());

            return ServiceResponse.Success(Validation.CategoryCreated);
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == id);
            if (category == null) return ServiceResponse.Error($"Category with id:{id} not found.");

            await _categoryRepository.Delete(category);
            return ServiceResponse.Success("Category deleted.");
        }

        public async Task<ServiceResponse> Edit(EditCategoryRequestDto model)
        {
            var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == model.Id);
            if (category == null) return ServiceResponse.Error($"Category with id:{model.Id} not found.");

            var updatedCategory = _categoryMapperService.MapEditCategoryRequestDto(model, category);
            await _categoryRepository.Edit(updatedCategory);
            return ServiceResponse.Success("Category edited.");
        }

        public async Task<ServiceResponse> GetAll()
        {
            var categories = await _categoryRepository.FindAll();
            var getCategoriesResponseDto = new List<GetCategoryResponseDto>();
            foreach(Category category in categories) getCategoriesResponseDto.Add(_categoryMapperService.MapGetCategoryResponseDto(category));
            return ServiceResponse<List<GetCategoryResponseDto>>.Success(getCategoriesResponseDto, "Categories retrieved.");
        }

        public async Task<ServiceResponse> GetById(int id)
        {
            var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == id);
            if (category == null) return ServiceResponse.Error($"Category with id:{id} not found.");

            var getCategoryResponseDto = _categoryMapperService.MapGetCategoryResponseDto(category);
            return ServiceResponse<GetCategoryResponseDto>.Success(getCategoryResponseDto, "Category retrieved.");
        }
    }
}
