namespace Smakoowa_Api.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Category>> FindAll()
        {
            return await _context.Set<Category>()
                .Include(c => c.Recipes)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Category>> FindByConditions(Expression<Func<Category, bool>> expresion)
        {
            return await _context.Set<Category>()
                .Where(expresion)
                .Include(c => c.Recipes)
                .ToListAsync();
        }
    }
}
