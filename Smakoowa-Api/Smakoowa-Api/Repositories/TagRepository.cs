using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ICategoryRepository
    {
        public TagRepository(DataContext context) : base(context) { }
    }
}
