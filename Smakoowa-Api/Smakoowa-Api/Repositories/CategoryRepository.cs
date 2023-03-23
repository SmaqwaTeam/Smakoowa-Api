using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }
    }
}
