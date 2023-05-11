namespace Smakoowa_Api.Data.Configurations
{
    public static class CommentReplyConfiguration
    {
        public static void ConfigureCommentReply(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentReply>().HasKey(c => c.Id);

            modelBuilder.Entity<CommentReply>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.CommentReplies)
               .HasForeignKey(c => c.CreatorId);
        }
    }
}
