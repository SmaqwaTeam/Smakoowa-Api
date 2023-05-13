namespace Smakoowa_Api.Services
{
    public class RequestCounterService : IRequestCounterService
    {
        public async Task LogRequestCount(string controllerName, string actionName, string parameters)
        {
            BackgroundDataContext _dbContext = new BackgroundDataContext(new DbContextOptions<BackgroundDataContext>());

            var key = $"{controllerName}.{actionName}.{parameters}";
            var requestCount = _dbContext.RequestCounts.FirstOrDefault(x => x.ControllerName + "." + x.ActionName + "." + x.RemainingPath == key);

            if (requestCount == null)
            {
                _dbContext.RequestCounts.Add(
                    new RequestCount
                    {
                        ControllerName = controllerName,
                        ActionName = actionName,
                        RemainingPath = parameters,
                        Count = 1
                    });
            }
            else
            {
                requestCount.Count++;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
