namespace Smakoowa_Api.Data.Configurations
{
    public static class RecipeConfiguration
    {
        public static void ConfigureRecipe(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(c => c.Id);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Instructions)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeComments)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Tags)
                .WithMany(t => t.Recipes);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Creator)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CreatorId);
        }
    }
}
