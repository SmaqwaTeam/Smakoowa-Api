namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRequestCounterService
    {
        Task LogRequestCount(string controllerName, string actionName, string parameters);
    }
}
