namespace Smakoowa_Api.Repositories
{
    public class RecipeCommentRepository : BaseRepository<RecipeComment>, IRecipeCommentRepository
    {
        public RecipeCommentRepository(DataContext context) : base(context) { }

        public override async Task Delete(RecipeComment recipeComment)
        {
            _context.RemoveRange(recipeComment.CommentReplies);
            _context.Remove(recipeComment);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<RecipeComment>> FindAll()
        {
            return await _context.Set<RecipeComment>()
            .Include(i => i.Recipe)
            .Include(i => i.CommentReplies)
            .ToListAsync();
        }

        public override async Task<IEnumerable<RecipeComment>> FindByConditions(Expression<Func<RecipeComment, bool>> expression)
        {
            return await _context.Set<RecipeComment>()
            .Where(expression)
            .Include(i => i.Recipe)
            .Include(i => i.CommentReplies)
            .ToListAsync();
        }

        public override async Task<RecipeComment> FindByConditionsFirstOrDefault(Expression<Func<RecipeComment, bool>> expresion)
        {
            return await _context.Set<RecipeComment>().Where(expresion)
                .Include(i => i.Recipe)
                .Include(i => i.CommentReplies)
                .FirstOrDefaultAsync();
        }
    }
}
