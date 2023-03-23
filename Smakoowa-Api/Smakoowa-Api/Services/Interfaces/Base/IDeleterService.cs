using Smakoowa_Api.Models.Services;

namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface IDeleterService
    {
        public Task<ServiceResponse> Delete(int id);
    }
}
