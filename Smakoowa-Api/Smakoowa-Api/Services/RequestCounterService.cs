using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Smakoowa_Api.Models.DatabaseModels.Statistics;

namespace Smakoowa_Api.Services
{
    public class RequestCounterService : IRequestCounterService
    {
        public async Task LogRequestCount(string controllerName, string actionName, string parameters)
        {
            BackgroundDataContext _dbContext1 = new BackgroundDataContext(new DbContextOptions<BackgroundDataContext>());
            //using (_dbContext1)
            //{
                var key = $"{controllerName}.{actionName}.{parameters}";
                var requestCount = _dbContext1.RequestCounts.FirstOrDefault(x => x.ControllerName + "." + x.ActionName + "." + x.RemainingPath == key);
                if (requestCount == null)
                {
                    requestCount = new RequestCount { ControllerName = controllerName, ActionName = actionName, RemainingPath = parameters, Count = 1 };
                    _dbContext1.RequestCounts.Add(requestCount);
                }
                else
                {
                    requestCount.Count++;
                }
                await _dbContext1.SaveChangesAsync();
            //}
        }
    }
}
