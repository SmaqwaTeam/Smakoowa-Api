namespace Smakoowa_Api.Repositories.Interfaces
{
    public interface IRequestCountRepository
    {
        public Task<IEnumerable<RequestCount>> FindAll();
        public Task<RequestCount> FindByConditionsFirstOrDefault(Expression<Func<RequestCount, bool>> expresion);
    }
}
