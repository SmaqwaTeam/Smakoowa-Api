using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICategoryService : ICreatorService<CategoryRequestDto>, IEditorService<CategoryRequestDto>, IDeleterService, IGetterService
    {
    }
}
