namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class BaseValidatorService
    {
        protected readonly IConfiguration _configuration;
        protected readonly int MaxNameLength;
        protected readonly int MinNameLength;

        protected BaseValidatorService(IConfiguration configuration, string configSectionName)
        {
            _configuration = configuration;
            MaxNameLength = int.Parse(_configuration.GetSection($"{configSectionName}:MaxNameLength").Value);
            MinNameLength = int.Parse(_configuration.GetSection($"{configSectionName}:MinNameLength").Value);
        }

        protected ServiceResponse ValidateNameLength(string name, string entityName)
        {
            if (name.Length < MinNameLength)
            {
                return ServiceResponse.Error($"{entityName} name must have a minimum of {MinNameLength} characters.");
            }

            if (name.Length > MaxNameLength)
            {
                return ServiceResponse.Error($"{entityName} name must have a maximum of {MaxNameLength} characters.");
            }

            return ServiceResponse.Success();
        }
    }
}
