using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ITagValidatorService
    {
        public Task<ServiceResponse> ValidateTagRequestDto(TagRequestDto tagRequestDto);
    }
}
