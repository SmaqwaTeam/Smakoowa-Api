namespace Smakoowa_Api.Services.MapperServices
{
    public class CategoryMapperService : ICategoryMapperService
    {
        private readonly IMapper _mapper;

        public CategoryMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Category MapCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto)
        {
            return _mapper.Map<Category>(createCategoryRequestDto);
        }

        public GetCategoryResponseDto MapGetCategoryResponseDto(Category category)
        {
            return _mapper.Map<GetCategoryResponseDto>(category);
        }

        public Category MapEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto, Category editedCategory)
        {
            editedCategory.Name = editCategoryRequestDto.Name;
            return editedCategory;
        }
    }
}
