namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagService : ICreatorService<TagRequestDto>, IEditorService<TagRequestDto>, IDeleterService, IGetterService
    {
        public Task<ServiceResponse> GetByIds(List<int> tagIds);
        public Task<ServiceResponse> GetByType(TagType tagType);
        public Task<ServiceResponse> GetUserLikedTags(); 
    }
}
