namespace Smakoowa_Api.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Ingredient>> FindAll()
        {
            return await _context.Set<Ingredient>()
            .Include(i => i.Recipe)
            .ToListAsync();
        }

        public override async Task<IEnumerable<Ingredient>> FindByConditions(Expression<Func<Ingredient, bool>> expression)
        {
            return await _context.Set<Ingredient>()
            .Where(expression)
            .Include(i => i.Recipe)
            .ToListAsync();
        }
    }
}
