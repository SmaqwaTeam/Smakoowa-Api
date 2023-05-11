using Smakoowa_Api.Data.Configurations;

namespace Smakoowa_Api.Data
{
    public class BackgroundDataContext : DbContext
    {
        public BackgroundDataContext(DbContextOptions<BackgroundDataContext> options) : base(options) { }

        public DbSet<RequestCount> RequestCounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SmakoowaApiDBConnectionBackground");
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RequestCountConfiguration.ConfigureRequestCount(modelBuilder);
        }
    }
}
