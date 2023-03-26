namespace Smakoowa_Api.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DataContext context) : base(context) { }
    }
}
