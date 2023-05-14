namespace Smakoowa_Api.Services
{
    public class ApiUserValidatorService : IApiUserValidatorService
    {
        private readonly int _maxNameLength;
        private readonly int _minNameLength;
        private readonly int _maxEmailLength;

        public ApiUserValidatorService(IConfiguration configuration)
        {
            _maxNameLength = int.Parse(configuration.GetSection($"Validation:RegisterUser:MaxNameLength").Value);
            _minNameLength = int.Parse(configuration.GetSection($"Validation:RegisterUser:MinNameLength").Value);
            _maxEmailLength = int.Parse(configuration.GetSection($"Validation:RegisterUser:MaxEmailLength").Value);
        }

        public async Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest)
        {
            if(registerRequest.Email.Length>_maxEmailLength)
            {
                return ServiceResponse.Error($"Email length too long. (max length: {_maxEmailLength})", HttpStatusCode.BadRequest);
            }

            if (registerRequest.UserName.Length > _maxNameLength)
            {
                return ServiceResponse.Error($"Username length too long. (max length: {_maxNameLength})", HttpStatusCode.BadRequest);
            }

            if (registerRequest.UserName.Length < _minNameLength)
            {
                return ServiceResponse.Error($"Username length too short. (min length: {_minNameLength})", HttpStatusCode.BadRequest);
            }

            return ServiceResponse.Success();
        }
    }
}
