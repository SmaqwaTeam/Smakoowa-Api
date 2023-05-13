namespace Smakoowa_Api.Services.MapperServices
{
    public class RequestCountMapperService : IRequestCountMapperService
    {
        public List<ControllerStatisticsResponseDto> MapGetControllerStatisticsResponseDto(IEnumerable<RequestCount> requestCounts)
        {
            var controllerNames = requestCounts.Select(r => r.ControllerName).Distinct().ToList();
            List<ControllerStatisticsResponseDto> controllerStatisticsResponseDtos = new();
            foreach (string controllerName in controllerNames)
                controllerStatisticsResponseDtos.Add(new ControllerStatisticsResponseDto { ControllerName = controllerName });

            foreach (RequestCount requestCount in requestCounts)
            {
                controllerStatisticsResponseDtos.FirstOrDefault(c => c.ControllerName == requestCount.ControllerName)
                    .ActionStatistics.Add
                    (new ActionStatisticsResponseDto
                    {
                        ActionName = requestCount.ActionName,
                        Parameters = requestCount.RemainingPath,
                        RequestCount = requestCount.Count
                    });
            }

            return controllerStatisticsResponseDtos;
        }
    }
}
