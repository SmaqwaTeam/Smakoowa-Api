namespace Smakoowa_Api.Data.Configurations
{
    public static class RecipeCommentConfiguration
    {
        public static void ConfigureRecipeComment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeComment>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeComment>()
                .HasMany(r => r.CommentReplies)
                .WithOne(c => c.RepliedComment)
                .HasForeignKey(c => c.RepliedCommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeComment>()
                .HasOne(r => r.Creator)
                .WithMany(c => c.RecipeComments)
                .HasForeignKey(c => c.CreatorId);
        }
    }
}
