using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagService : ICreatorService<TagRequestDto>, IEditorService<TagRequestDto>, IDeleterService, IGetterService
    {
    }
}
