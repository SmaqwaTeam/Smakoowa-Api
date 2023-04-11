namespace Smakoowa_Api.Models.ResponseDtos
{
    public class ControllerStatisticsResponseDto
    {
        public string ControllerName { get; set; }
        public List<ActionStatisticsResponseDto> ActionStatistics { get; set; } = new();
    }
}
