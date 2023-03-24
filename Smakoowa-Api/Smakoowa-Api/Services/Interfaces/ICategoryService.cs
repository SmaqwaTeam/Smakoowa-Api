namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICategoryService : ICreatorService<CreateCategoryRequestDto>, IUpdaterService<EditCategoryRequestDto>, IDeleterService, IGetterService
    {
    }
}
