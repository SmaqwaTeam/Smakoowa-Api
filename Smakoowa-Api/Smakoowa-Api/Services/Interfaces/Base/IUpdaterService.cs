using Smakoowa_Api.Models.Services;

namespace Smakoowa_Api.Services.Interfaces.Base
{
    public interface IUpdaterService
    {
        public Task<ServiceResponse> Edit(IRequestDto model);
    }
}
