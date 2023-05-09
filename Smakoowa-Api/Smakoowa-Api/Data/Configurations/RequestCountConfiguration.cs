namespace Smakoowa_Api.Data.Configurations
{
    public static class RequestCountConfiguration
    {
        public static void ConfigureRequestCount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestCount>().HasKey(c => c.Id);
            modelBuilder.Entity<RequestCount>().Property(r => r.RemainingPath).IsRequired(false);
        }
    }
}
