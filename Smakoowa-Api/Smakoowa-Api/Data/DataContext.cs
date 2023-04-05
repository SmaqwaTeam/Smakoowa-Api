using Smakoowa_Api.Data.Configurations;
using Smakoowa_Api.Data.DatabaseSeeds;

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

            UserSeed.SeedUsers(modelBuilder);
            TagSeed.SeedTags(modelBuilder);
            CategorySeed.SeedCategories(modelBuilder);

            CategoryConfiguration.ConfigureCategories(modelBuilder);
            RecipeConfiguration.ConfigureRecipe(modelBuilder);
            IngredientConfiguration.ConfigureIngredient(modelBuilder);
            InstructionConfiguration.ConfigureInstruction(modelBuilder);
            TagConfiguration.ConfigureTag(modelBuilder);

            RecipeCommentConfiguration.ConfigureRecipeComment(modelBuilder);
            CommentReplyConfiguration.ConfigureCommentReply(modelBuilder);

            RecipeLikeConfiguration.ConfigureRecipeLike(modelBuilder);
            RecipeCommentLikeConfiguration.ConfigureRecipeCommentLike(modelBuilder);
            CommentReplyLikeConfiguration.ConfigureCommentReplyLike(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUserId = _apiUserService.GetCurrentUserId();

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
