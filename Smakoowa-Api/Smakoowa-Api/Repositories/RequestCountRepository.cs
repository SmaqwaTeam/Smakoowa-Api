namespace Smakoowa_Api.Repositories
{
    public class RequestCountRepository : IRequestCountRepository
    {
        protected readonly BackgroundDataContext _context;

        public RequestCountRepository(BackgroundDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestCount>> FindAll()
        {
            return await _context.Set<RequestCount>().ToListAsync();
        }

        public async Task<RequestCount> FindByConditionsFirstOrDefault(Expression<Func<RequestCount, bool>> expresion)
        {
            return await _context.Set<RequestCount>().Where(expresion).FirstOrDefaultAsync();
        }
    }
}
