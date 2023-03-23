using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, ICategoryRepository
    {
        public RecipeRepository(DataContext context) : base(context) { }
    }
}
