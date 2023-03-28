namespace Smakoowa_Api.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(DataContext context) : base(context) { }
    }
}
