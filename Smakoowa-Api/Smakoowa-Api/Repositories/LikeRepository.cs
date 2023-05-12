namespace Smakoowa_Api.Repositories
{
    public class LikeRepository<T> : BaseRepository<Like>, ILikeRepository<Like> where T : Like
    {
        public LikeRepository(DataContext context) : base(context) { }
    }
}
