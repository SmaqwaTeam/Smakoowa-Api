using Smakoowa_Api.Models.DatabaseModels.Statistics;

namespace Smakoowa_Api.Data.Configurations
{
    public class RequestCountConfiguration
    {
        public static void ConfigureRequestCount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestCount>().HasKey(x => new { x.ControllerName, x.ActionName, x.RemainingPath });
        }
    }
}
