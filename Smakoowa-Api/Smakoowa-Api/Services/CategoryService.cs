namespace Smakoowa_Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapperService _categoryMapperService;
        private readonly ICategoryValidatorService _categoryValidatorService;
        private readonly IHelperService<CategoryService> _helperService;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryMapperService categoryMapperService,
            ICategoryValidatorService categoryValidatorService, IHelperService<CategoryService> helperService)
        {
            _categoryRepository = categoryRepository;
            _categoryMapperService = categoryMapperService;
            _categoryValidatorService = categoryValidatorService;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> Create(CategoryRequestDto categoryRequestDto)
        {
            var validationResult = await _categoryValidatorService.ValidateCategoryRequestDto(categoryRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var category = _categoryMapperService.MapCreateCategoryRequestDto(categoryRequestDto);

            try
            {
                await _categoryRepository.Create(category);
                return ServiceResponse.Success("Category created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a category.");
            }
        }

        public async Task<ServiceResponse> Delete(int categoryId)
        {
            var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == categoryId);
            if (category == null) return ServiceResponse.Error($"Category with id: {categoryId} not found.");

            try
            {
                await _categoryRepository.Delete(category);
                return ServiceResponse.Success("Category deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a category.");
            }
        }

        public async Task<ServiceResponse> Edit(CategoryRequestDto categoryRequestDto, int categoryId)
        {
            var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == categoryId);
            if (category == null) return ServiceResponse.Error($"Category with id: {categoryId} not found.");

            var validationResult = await _categoryValidatorService.ValidateCategoryRequestDto(categoryRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var updatedCategory = _categoryMapperService.MapEditCategoryRequestDto(categoryRequestDto, category);

            try
            {
                await _categoryRepository.Edit(updatedCategory);
                return ServiceResponse.Success("Category edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a category.");
            }
        }

        public async Task<ServiceResponse> GetAll()
        {
            try
            {
                var categories = await _categoryRepository.FindAll();
                var getCategoriesResponseDto = new List<GetCategoryResponseDto>();
                foreach (Category category in categories) getCategoriesResponseDto.Add(_categoryMapperService.MapGetCategoryResponseDto(category));
                return ServiceResponse<List<GetCategoryResponseDto>>.Success(getCategoriesResponseDto, "Categories retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the categories.");
            }
        }

        public async Task<ServiceResponse> GetById(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.FindByConditionsFirstOrDefault(c => c.Id == categoryId);
                if (category == null) return ServiceResponse.Error($"Category with id: {categoryId} not found.");

                var getCategoryResponseDto = _categoryMapperService.MapGetCategoryResponseDto(category);
                return ServiceResponse<GetCategoryResponseDto>.Success(getCategoryResponseDto, "Category retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the category.");
            }
        }
    }
}
