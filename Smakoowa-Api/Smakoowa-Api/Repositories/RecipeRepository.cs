using System.Linq;

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
                .Include(r => r.Ingredients)  //to be implemented
                .Include(r => r.Instructions)
                //.Include(r => r.Likes)
                //.Include(r => r.RecipeComments)
                .FirstOrDefaultAsync();
        }

        public override async Task Delete(Recipe recipe)
        {
            _context.RemoveRange(recipe.Ingredients);
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
                .Include(r => r.Ingredients)  //to be implemented
                .Include(r => r.Instructions)
                //.Include(r => r.Likes)
                //.Include(r => r.RecipeComments)
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
                .Include(r => r.Ingredients)  //to be implemented
                .Include(r => r.Instructions)
                //.Include(r => r.Likes)
                //.Include(r => r.RecipeComments)
                .ToListAsync();
        }
    }
}
