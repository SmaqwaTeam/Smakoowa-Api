namespace Smakoowa_Api.Services.Interfaces
{
    public interface IControllerStatisticsService
    {
        public Task<ServiceResponse> GetAll();
        public Task<int> GetRecipeViewCount(int recipeId);
    }
}
