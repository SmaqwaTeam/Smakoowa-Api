namespace Smakoowa_Api.Data.Configurations
{
    public class CategoryConfiguration
    {
        public static void ConfigureCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
        }
    }
}
