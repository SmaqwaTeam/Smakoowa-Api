namespace Smakoowa_Api.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(DataContext context) : base(context) { }
    }
}
