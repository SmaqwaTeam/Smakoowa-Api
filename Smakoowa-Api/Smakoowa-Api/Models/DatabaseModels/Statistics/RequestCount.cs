namespace Smakoowa_Api.Models.DatabaseModels.Statistics
{
    public class RequestCount : IDbModel
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string RemainingPath { get; set; }
        public int Count { get; set; }
    }
}
