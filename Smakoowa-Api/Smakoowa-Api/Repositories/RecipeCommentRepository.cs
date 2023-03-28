using Smakoowa_Api.Models.DatabaseModels.Comments;

namespace Smakoowa_Api.Repositories
{
    public class RecipeCommentRepository : BaseRepository<RecipeComment>, IRecipeCommentRepository
    {
        public RecipeCommentRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<RecipeComment>> FindAll()
        {
            return await _context.Set<RecipeComment>()
            .Include(i => i.Recipe)
            .ToListAsync();
        }

        public override async Task<IEnumerable<RecipeComment>> FindByConditions(Expression<Func<RecipeComment, bool>> expression)
        {
            return await _context.Set<RecipeComment>()
            .Where(expression)
            .Include(i => i.Recipe)
            .ToListAsync();
        }
    }
}
