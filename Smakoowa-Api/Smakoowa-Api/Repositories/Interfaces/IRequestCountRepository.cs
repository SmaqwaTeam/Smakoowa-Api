using Smakoowa_Api.Models.DatabaseModels.Statistics;

namespace Smakoowa_Api.Repositories.Interfaces
{
    public interface IRequestCountRepository
    {
        public Task<IEnumerable<RequestCount>> FindAll();
    }
}
