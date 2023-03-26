using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ICategoryMapperService
    {
        public Category MapCreateCategoryRequestDto(CategoryRequestDto categoryRequestDto);
        public GetCategoryResponseDto MapGetCategoryResponseDto(Category category);
        public Category MapEditCategoryRequestDto(CategoryRequestDto categoryRequestDto, Category editedCategory);
    }
}
