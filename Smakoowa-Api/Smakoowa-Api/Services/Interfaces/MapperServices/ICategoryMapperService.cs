namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ICategoryMapperService
    {
        public Category MapCreateCategoryRequestDto(CategoryRequestDto categoryRequestDto);
        public CategoryResponseDto MapGetCategoryResponseDto(Category category);
        public Category MapEditCategoryRequestDto(CategoryRequestDto categoryRequestDto, Category editedCategory);
    }
}
