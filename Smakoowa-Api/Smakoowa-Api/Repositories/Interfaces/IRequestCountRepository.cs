namespace Smakoowa_Api.Repositories.Interfaces
{
    public interface IRequestCountRepository : IRepository
    {
        public Task<IEnumerable<RequestCount>> FindAll();
        public Task<RequestCount> FindByConditionsFirstOrDefault(Expression<Func<RequestCount, bool>> expresion);
    }
}
