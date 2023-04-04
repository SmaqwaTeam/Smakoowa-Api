namespace Smakoowa_Api.Data
{
    public class DataContext : IdentityDbContext<ApiUser, ApiRole, int>
    {
        private readonly IApiUserService _apiUserService;

        public DataContext(DbContextOptions<DataContext> options, IApiUserService apiUserService) : base(options)
        {
            _apiUserService = apiUserService;
        }

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
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser { Id = 1, UserName = "PlaceholderAdmin" },
                new ApiUser { Id = 2, UserName = "PlaceholderUser" }
            );

            modelBuilder.Entity<ApiRole>().HasData(
                new ApiRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new ApiRole { Id = 2, Name = "User", NormalizedName = "USER" }
                );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }
                );

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
                .HasOne(l => l.LikedRecipe)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentReplyLike>()
                .HasOne(l => l.LikedCommentReply)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.CommentReplyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeCommentLike>()
                .HasOne(l => l.LikedRecipeComment)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.RecipeCommentId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUserId = await _apiUserService.GetCurrentUserId();

            foreach (EntityEntry<Creatable> entry in ChangeTracker.Entries<Creatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorId = currentUserId;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }

            foreach (EntityEntry<Updatable> entry in ChangeTracker.Entries<Updatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdaterId = currentUserId;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
