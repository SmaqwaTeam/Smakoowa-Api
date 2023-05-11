namespace Smakoowa_Api.Data.Configurations
{
    public static class IngredientConfiguration
    {
        public static void ConfigureIngredient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasKey(c => c.Id);
        }
    }
}
