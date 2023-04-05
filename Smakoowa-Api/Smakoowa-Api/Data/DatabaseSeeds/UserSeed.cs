namespace Smakoowa_Api.Data.DatabaseSeeds
{
    public class UserSeed
    {
        public static void SeedUsers(ModelBuilder modelBuilder)
        {
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
        }
    }
}
