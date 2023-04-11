namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IRequestCountMapperService
    {
        public List<ControllerStatisticsResponseDto> MapGetControllerStatisticsResponseDto(IEnumerable<RequestCount> requestCounts);
    }
}
