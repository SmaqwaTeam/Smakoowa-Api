﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Smakoowa_Api.Models.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Smakoowa_Api.Models.Auditables;
using Smakoowa_Api.Models.DatabaseModels.Likes;

namespace Smakoowa_Api.Data
{
    public class DataContext : IdentityDbContext<ApiUser, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<CommentReplyLike> CommentReplyLikes { get; set; }
        public DbSet<RecipeLike> RecipeLikes { get; set; }
        public DbSet<RecipeCommentLike> RecipeCommentLikes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeComment> RecipeComments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SmakoowaApiDBConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Recipe>().HasKey(c => c.Id);

            modelBuilder.Entity<CommentReply>().HasKey(c => c.Id);

            modelBuilder.Entity<Ingredient>().HasKey(c => c.Id);

            modelBuilder.Entity<Instruction>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeLike>().HasKey(c => c.Id);

            modelBuilder.Entity<CommentReplyLike>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeCommentLike>().HasKey(c => c.Id);

            modelBuilder.Entity<RecipeComment>().HasKey(c => c.Id);

            modelBuilder.Entity<Tag>().HasKey(c => c.Id);

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

            modelBuilder.Entity<RecipeComment>()
                .HasMany(r => r.CommentReplies)
                .WithOne(c => c.RepliedComment)
                .HasForeignKey(c => c.RepliedCommentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeComment>()
                .HasOne(r => r.Creator)
                .WithMany(c => c.RecipeComments)
                .HasForeignKey(c => c.CreatorId);

            modelBuilder.Entity<CommentReply>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.CommentReplies)
               .HasForeignKey(c => c.CreatorId);

            modelBuilder.Entity<RecipeLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.RecipeLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<CommentReplyLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.CommentReplyLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<RecipeCommentLike>()
               .HasOne(r => r.Creator)
               .WithMany(c => c.RecipeCommentLikes)
               .HasForeignKey(l => l.CreatorId);

            modelBuilder.Entity<RecipeLike>()
                .HasOne(l => l.Recipe)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentReplyLike>()
                .HasOne(l => l.CommentReply)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.CommentReplyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeCommentLike>()
                .HasOne(l => l.RecipeComment)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RecipeCommentId)
                .OnDelete(DeleteBehavior.NoAction);




            //modelBuilder.Entity<CommentReply>()
            //    .HasMany(c => c.Likes)
            //    .WithOne(l => (CommentReply)l.LikedContent)
            //    .HasForeignKey(l => l.LikedContentId);

            //modelBuilder.Entity<RecipeComment>()
            //    .HasMany(r => r.Likes)
            //    .WithOne(l => (RecipeComment)l.LikedContent)
            //    .HasForeignKey(r => r.LikedContentId);

            //modelBuilder.Entity<Recipe>()
            //    .HasMany(r => r.Likes)
            //    .WithOne(l => (Recipe)l.LikedContent)
            //    .HasForeignKey(r => r.LikedContentId);


            //modelBuilder.Entity<Like>()
            //    .HasOne(l => (Recipe)l.LikedContent)
            //    .WithMany(c => c.Likes)
            //    .HasForeignKey(l => l.LikedContentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Like>()
            //    .HasOne(l => (RecipeComment)l.LikedContent)
            //    .WithMany(c => c.Likes)
            //    .HasForeignKey(l => l.LikedContentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Like>()
            //    .HasOne(l => (CommentReply)l.LikedContent)
            //    .WithMany(c => c.Likes)
            //    .HasForeignKey(l => l.LikedContentId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int userId = 1;
            foreach (EntityEntry<Creatable> entry in ChangeTracker.Entries<Creatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorId = userId;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }

            foreach (EntityEntry<Updatable> entry in ChangeTracker.Entries<Updatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdaterId = userId;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
