namespace Smakoowa_Api.Data.Configurations
{
    public static class CommentReplyLikeConfiguration
    {
        public static void ConfigureCommentReplyLike(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentReplyLike>().HasKey(c => c.Id);

            modelBuilder.Entity<CommentReplyLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.CommentReplyLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<CommentReplyLike>()
                .HasOne(l => l.LikedCommentReply)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.CommentReplyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
