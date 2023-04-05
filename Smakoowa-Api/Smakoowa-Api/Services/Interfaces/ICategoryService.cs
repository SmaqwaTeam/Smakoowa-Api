namespace Smakoowa_Api.Services.Interfaces
{
    public interface ICategoryService : ICreatorService<CategoryRequestDto>, IEditorService<CategoryRequestDto>, IDeleterService, IGetterService
    {
        public Task<ServiceResponse> GetByIds(List<int> categoryIds);
    }
}
