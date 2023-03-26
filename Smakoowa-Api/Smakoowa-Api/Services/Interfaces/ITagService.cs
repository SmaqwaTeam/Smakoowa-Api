namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagService : ICreatorService<CreateTagRequestDto>, IUpdaterService<EditTagRequestDto>, IDeleterService, IGetterService
    {
    }
}
