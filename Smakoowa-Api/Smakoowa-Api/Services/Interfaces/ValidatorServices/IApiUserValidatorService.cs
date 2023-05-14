namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface IApiUserValidatorService
    {
        public Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest);
    }
}
