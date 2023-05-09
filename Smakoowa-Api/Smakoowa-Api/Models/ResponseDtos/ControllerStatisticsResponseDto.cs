namespace Smakoowa_Api.Models.ResponseDtos
{
    public class ControllerStatisticsResponseDto : IResponseDto
    {
        public string ControllerName { get; set; }
        public List<ActionStatisticsResponseDto> ActionStatistics { get; set; } = new();
    }
}
