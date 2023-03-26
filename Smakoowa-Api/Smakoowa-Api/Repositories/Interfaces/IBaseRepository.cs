namespace ModernPantryBackend.Interfaces
{
    public interface IBaseRepository<T> : IRepository where T : IDbModel
    {
        public Task<T> Create(T model);
        public Task Delete(T model);
        public Task Edit(T model);
        public Task<IEnumerable<T>> FindAll();
        public Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion);
        public Task<T> FindByConditionsFirstOrDefault(Expression<Func<T, bool>> expresion);
        public Task<bool> CheckIfExists(Expression<Func<T, bool>> expresion);
    }
}
