namespace Smakoowa_Api.Data.Configurations
{
    public static class RecipeCommentLikeConfiguration
    {
        public static void ConfigureRecipeCommentLike(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCommentLike>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeCommentLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.RecipeCommentLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<RecipeCommentLike>()
                .HasOne(l => l.LikedRecipeComment)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.LikedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
