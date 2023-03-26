namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ITagValidatorService
    {
        public Task<ServiceResponse> ValidateCreateTagRequestDto(CreateTagRequestDto createTagRequestDto);
        public Task<ServiceResponse> ValidateEditTagRequestDto(EditTagRequestDto editTagRequestDto);
    }
}
