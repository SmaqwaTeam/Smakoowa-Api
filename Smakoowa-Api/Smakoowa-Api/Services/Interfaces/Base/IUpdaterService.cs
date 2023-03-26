namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface IEditorService<T> where T : IRequestDto
    {
        public Task<ServiceResponse> Edit(T model, int modelId);
    }
}
