using Microsoft.AspNetCore.Mvc.Controllers;
using Smakoowa_Api.Services.Interfaces;

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

        public async Task Invoke(HttpContext context, ILogger<RequestCountMiddleware> logger, DataContext dbContext, IBackgroundTaskQueue backgroundTaskQueue, IRequestCounterService requestCounterService)
        {
            //await _next.Invoke(context);
            _context = context;
            _requestCounterService = requestCounterService;
            // Get the endpoint that was called
            await backgroundTaskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);

            await _next(_context);

            //var endpoint = _context.GetEndpoint();
            //if (endpoint == null)
            //{
            //    // The request did not match any endpoint, so just pass it through
            //    await _next(_context);
            //    return;
            //}

            //// Get the name of the controller and action method
            //var controllerName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
            //var actionName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;

            //// Get the remaining parameters
            //var remainingPath = _context.Request.Path.ToString().Substring(("/api/" + controllerName + "/" + actionName + "/").Length).TrimStart('/');

            //// Build the key for the request count
            //var key = $"{controllerName}.{actionName}.{remainingPath}";

            //// Increment the request count for the corresponding endpoint and parameters in the database
            //var requestCount = dbContext.RequestCounts.FirstOrDefault(x => x.ControllerName + "." + x.ActionName + "." + x.RemainingPath == key);
            //if (requestCount == null)
            //{
            //    requestCount = new RequestCount { ControllerName = controllerName, ActionName = actionName, RemainingPath = remainingPath, Count = 1 };
            //    dbContext.RequestCounts.Add(requestCount);
            //}
            //else
            //{
            //    requestCount.Count++;
            //}
            //await dbContext.SaveChangesAsync();

            ////Thread.Sleep(10000);
            //// Call the next middleware in the pipeline
            //await _next(context);
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            //_logger.LogInformation("123");
            //await requestCounterService.LogRequestCount("Category", "GetById", "1");

            var endpoint = _context.GetEndpoint();
            if (endpoint == null)
            {
                // The request did not match any endpoint, so just pass it through
                //await _next(_context);
                return;
            }

            // Get the name of the controller and action method
            var controllerName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
            var actionName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;

            // Get the remaining parameters
            var remainingPath = _context.Request.Path.ToString().Substring(("/api/" + controllerName + "/" + actionName + "/").Length).TrimStart('/');
            await _requestCounterService.LogRequestCount(controllerName, actionName, remainingPath);
        }
    }
}
