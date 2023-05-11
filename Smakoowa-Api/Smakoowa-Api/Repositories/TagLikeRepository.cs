namespace Smakoowa_Api.Repositories
{
    public class TagLikeRepository : BaseRepository<TagLike>, ITagLikeRepository
    {
        public TagLikeRepository(DataContext context) : base(context) { }
    }
}
