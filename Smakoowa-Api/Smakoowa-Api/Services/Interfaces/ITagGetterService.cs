namespace Smakoowa_Api.Services.Interfaces
{
    public interface ITagGetterService : IGetterService
    {
        public Task<ServiceResponse> GetByIds(List<int> tagIds);
        public Task<ServiceResponse> GetByType(TagType tagType);
        public Task<ServiceResponse> GetUserLikedTags();
    }
}
