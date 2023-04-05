namespace Smakoowa_Api.Data.Configurations
{
    public class TagConfiguration
    {
        public static void ConfigureTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasKey(c => c.Id);
        }
    }
}
