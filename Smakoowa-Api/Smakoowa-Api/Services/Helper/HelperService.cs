using Smakoowa_Api.Services.Interfaces.Helper;

namespace Smakoowa_Api.Services.Helper
{
    public class HelperService<T> : IHelperService<T> where T : class
    {
        private readonly ILogger<T> _logger;

        public HelperService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public ServiceResponse HandleException(Exception exception, string message)
        {
            _logger.LogError(exception.Message + "\nStack trace: " + exception.StackTrace, exception);

            if (exception.InnerException != null)
                _logger.LogError(
                    exception.InnerException.Message
                    + "\nStack trace: " + exception.InnerException.StackTrace, exception.InnerException);

            return ServiceResponse.Error(message, HttpStatusCode.InternalServerError);
        }
    }
}
