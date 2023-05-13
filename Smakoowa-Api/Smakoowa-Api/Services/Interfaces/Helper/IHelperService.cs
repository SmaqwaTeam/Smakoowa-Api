namespace Smakoowa_Api.Services.Interfaces.Helper
{
    public interface IHelperService<T> where T : class
    {
        public ServiceResponse HandleException(Exception exception, string message = "");
    }
}
