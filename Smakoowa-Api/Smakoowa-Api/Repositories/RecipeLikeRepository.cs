namespace Smakoowa_Api.Repositories
{
    public class RecipeLikeRepository : BaseRepository<RecipeLike>, IRecipeLikeRepository
    {
        public RecipeLikeRepository(DataContext context) : base(context) { }
    }
}
