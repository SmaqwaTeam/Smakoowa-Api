namespace Smakoowa_Api.Repositories
{
    public class RecipeCommentLikeRepository : BaseRepository<RecipeCommentLike>, IRecipeCommentLikeRepository
    {
        public RecipeCommentLikeRepository(DataContext context) : base(context) { }
    }
}
