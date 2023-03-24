namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface IUpdaterService<T> where T : IRequestDto
    {
        public Task<ServiceResponse> Edit(T model);
    }
}
