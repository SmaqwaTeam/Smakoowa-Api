namespace Smakoowa_Api.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<Tag>> FindAll()
        {
            return await _context.Set<Tag>()
                .Include(c => c.Recipes)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Tag>> FindByConditions(Expression<Func<Tag, bool>> expresion)
        {
            return await _context.Set<Tag>()
                .Where(expresion)
                .Include(c => c.Recipes)
                .ToListAsync();
        }
    }
}
