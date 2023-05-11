namespace ModernPantryBackend.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IDbModel
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CheckIfExists(Expression<Func<T, bool>> expresion)
        {
            if (await _context.Set<T>().Where(expresion).AnyAsync()) return true;
            else return false;
        }

        public virtual async Task<T> Create(T model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public virtual async Task Delete(T model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Edit(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion)
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }

        public virtual async Task<T> FindByConditionsFirstOrDefault(Expression<Func<T, bool>> expresion)
        {
            return await _context.Set<T>().Where(expresion).FirstOrDefaultAsync();
        }
    }
}
