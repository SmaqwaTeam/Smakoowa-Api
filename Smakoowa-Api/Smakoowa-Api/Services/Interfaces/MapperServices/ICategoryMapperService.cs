namespace Smakoowa_Api.Services.Interfaces.Mapper
{
    public interface ICategoryMapperService
    {
        public Category MapCreateCategoryRequestDto(CreateCategoryRequestDto createCategoryRequestDto);
        public GetCategoryResponseDto MapGetCategoryResponseDto(Category category);
        public Category MapEditCategoryRequestDto(EditCategoryRequestDto editCategoryRequestDto, Category editedCategory);
    }
}
