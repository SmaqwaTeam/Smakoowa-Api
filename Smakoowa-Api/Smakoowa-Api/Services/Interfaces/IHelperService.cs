namespace Smakoowa_Api.Services.Interfaces
{
    public interface IHelperService<T> where T : class
    {
        public ServiceResponse HandleException(Exception exception,string message = "");
    }
}
