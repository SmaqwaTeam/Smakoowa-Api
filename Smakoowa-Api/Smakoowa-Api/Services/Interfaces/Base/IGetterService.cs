namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface IGetterService
    {
        public Task<ServiceResponse> GetById(int id);
        public Task<ServiceResponse> GetAll();
    }
}
