namespace Smakoowa_Api.Models.ResponseDtos
{
    public class ActionStatisticsResponseDto : IResponseDto
    {
        public string ActionName { get; set; }
        public string Parameters { get; set; }
        public int RequestCount { get; set; }
    }
}
