using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class RecipeCommentRepository : BaseRepository<RecipeComment>, ICategoryRepository
    {
        public RecipeCommentRepository(DataContext context) : base(context) { }
    }
}
