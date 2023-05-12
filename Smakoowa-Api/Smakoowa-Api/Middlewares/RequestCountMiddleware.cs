using Microsoft.AspNetCore.Mvc.Controllers;
using Smakoowa_Api.Services.BackgroundTaskQueue;

namespace Smakoowa_Api.Middlewares
{
    public class RequestCountMiddleware
    {
        private readonly RequestDelegate _next;
        private HttpContext _context;
        private IRequestCounterService _requestCounterService;

        public RequestCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IBackgroundTaskQueue backgroundTaskQueue, IRequestCounterService requestCounterService)
        {
            _context = context;
            _requestCounterService = requestCounterService;
            await backgroundTaskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
            await _next(_context);
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            try
            {
                var endpoint = _context.GetEndpoint();
                if (endpoint == null) return;

                var controllerName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
                if (controllerName == "Statistics") return;

                var actionName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;

                var uriLength = ("/api/" + controllerName + "/" + actionName + "/").Length;
                var requestPath = _context.Request.Path.ToString();

                var remainingPath = uriLength >= requestPath.Length ? null : requestPath.Substring(uriLength).TrimStart('/');

                await _requestCounterService.LogRequestCount(controllerName, actionName, remainingPath);
            }
            catch
            {

            }
        }
    }
}
