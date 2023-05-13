namespace Smakoowa_Api.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(DataContext context) : base(context) { }

        public override async Task Edit(Recipe recipe)
        {
            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public override async Task<Recipe> FindByConditionsFirstOrDefault(Expression<Func<Recipe, bool>> expresion)
        {
            return await _context.Set<Recipe>().Where(expresion)
                .Include(r => r.Category)
                .Include(r => r.Creator)
                .Include(r => r.Updater)
                .Include(r => r.Tags)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Include(r => r.RecipeComments).ThenInclude(d => d.Likes)
                .Include(r => r.RecipeComments).ThenInclude(b => b.CommentReplies).ThenInclude(e => e.Likes)
                .FirstOrDefaultAsync();
        }

        public override async Task Delete(Recipe recipe)
        {
            _context.RemoveRange(recipe.Ingredients);
            foreach (var item in recipe.RecipeComments)
            {
                _context.RemoveRange(item.CommentReplies);
            }

            _context.RemoveRange(recipe.RecipeComments);
            _context.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Recipe>> FindAll()
        {
            return await _context.Set<Recipe>()
                .Include(r => r.Category)
                .Include(r => r.Creator)
                .Include(r => r.Updater)
                .Include(r => r.Tags)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Include(r => r.RecipeComments).ThenInclude(d => d.Likes)
                .Include(r => r.RecipeComments).ThenInclude(b => b.CommentReplies).ThenInclude(e => e.Likes)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Recipe>> FindByConditions(Expression<Func<Recipe, bool>> expresion)
        {
            return await _context.Set<Recipe>()
                .Where(expresion)
                .Include(r => r.Category)
                .Include(r => r.Creator)
                .Include(r => r.Updater)
                .Include(r => r.Tags)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Include(r => r.RecipeComments).ThenInclude(d => d.Likes)
                .Include(r => r.RecipeComments).ThenInclude(b => b.CommentReplies).ThenInclude(e => e.Likes)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> FindByConditions(Expression<Func<Recipe, bool>> expresion, int? count = null)
        {
            return await _context.Set<Recipe>()
                .Where(expresion)
                .Include(r => r.Category)
                .Include(r => r.Creator)
                .Include(r => r.Updater)
                .Include(r => r.Tags)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.Likes)
                .Include(r => r.RecipeComments).ThenInclude(d => d.Likes)
                .Include(r => r.RecipeComments).ThenInclude(b => b.CommentReplies).ThenInclude(e => e.Likes)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count ?? int.MaxValue)
                .ToListAsync();
        }
    }
}
