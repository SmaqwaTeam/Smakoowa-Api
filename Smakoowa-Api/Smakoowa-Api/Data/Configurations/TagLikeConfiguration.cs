namespace Smakoowa_Api.Data.Configurations
{
    public static class TagLikeConfiguration
    {
        public static void ConfigureRecipeLike(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagLike>().HasKey(c => c.Id);

            modelBuilder.Entity<TagLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.TagLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<TagLike>()
                .HasOne(l => l.LikedTag)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.LikedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
