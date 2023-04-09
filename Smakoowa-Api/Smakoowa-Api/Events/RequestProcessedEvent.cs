namespace Smakoowa_Api.Events
{
    public static class RequestProcessedEvent
    {
        public static event Action<string, string, string> RequestProcessed;

        public async static void OnRequestProcessed(string controllerName, string actionName, string parameters)
        {
            RequestProcessed?.Invoke(controllerName, actionName, parameters);
        }
    }

}
