namespace Smakoowa_Api.Data.Configurations
{
    public static class RecipeLikeConfiguration
    {
        public static void ConfigureRecipeLike(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeLike>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.RecipeLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<RecipeLike>()
                .HasOne(l => l.LikedRecipe)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.LikedId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
