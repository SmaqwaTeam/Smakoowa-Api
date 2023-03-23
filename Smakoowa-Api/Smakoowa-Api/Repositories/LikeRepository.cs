using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ICategoryRepository
    {
        public LikeRepository(DataContext context) : base(context) { }
    }
}
