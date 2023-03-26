namespace Smakoowa_Api.Repositories
{
    public class RecipeCommentRepository : BaseRepository<RecipeComment>, IRecipeCommentRepository
    {
        public RecipeCommentRepository(DataContext context) : base(context) { }
    }
}
