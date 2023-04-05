namespace Smakoowa_Api.Data.DatabaseSeeds
{
    public class CategorySeed
    {
        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Breakfast" },
                new Category { Id = 2, Name = "Soups" },
                new Category { Id = 3, Name = "Main courses" },
                new Category { Id = 4, Name = "Desserts" },
                new Category { Id = 5, Name = "Drinks" },
                new Category { Id = 6, Name = "Snacks" },
                new Category { Id = 7, Name = "Salads" },
                new Category { Id = 8, Name = "Preserves" },
                new Category { Id = 9, Name = "Additions" },
                new Category { Id = 10, Name = "Bakery" },
                new Category { Id = 11, Name = "Cold cuts" }
            );
        }
    }
}
