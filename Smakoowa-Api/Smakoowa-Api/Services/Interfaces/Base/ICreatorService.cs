namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface ICreatorService<T> where T : IRequestDto
    {
        public Task<ServiceResponse> Create(T model);
    }
}
