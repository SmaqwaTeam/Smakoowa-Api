using Smakoowa_Api.Models.Services;

namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface ICreatorService
    {
        public Task<ServiceResponse> Create(IRequestDto model);
    }
}
