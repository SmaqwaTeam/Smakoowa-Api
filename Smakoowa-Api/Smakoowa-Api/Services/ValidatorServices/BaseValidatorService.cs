namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class BaseValidatorService
    {
        protected readonly IConfiguration _configuration;
        protected readonly int _maxNameLength;
        protected readonly int _minNameLength;

        protected BaseValidatorService(IConfiguration configuration, string configSectionName)
        {
            _configuration = configuration;
            _maxNameLength = int.Parse(_configuration.GetSection($"{configSectionName}:MaxNameLength").Value);
            _minNameLength = int.Parse(_configuration.GetSection($"{configSectionName}:MinNameLength").Value);
        }

        protected ServiceResponse ValidateNameLength(string name, string entityName)
        {
            if (name.Length < _minNameLength)
            {
                return ServiceResponse.Error($"{entityName} name must have a minimum of {_minNameLength} characters.", HttpStatusCode.BadRequest);
            }

            if (name.Length > _maxNameLength)
            {
                return ServiceResponse.Error($"{entityName} name must have a maximum of {_maxNameLength} characters.", HttpStatusCode.BadRequest);
            }

            return ServiceResponse.Success();
        }
    }
}
