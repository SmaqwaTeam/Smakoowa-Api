namespace Smakoowa_Api.Repositories.Interfaces
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        public Task<IEnumerable<Recipe>> FindByConditions(Expression<Func<Recipe, bool>> expresion, int? count = null);
    }
}
