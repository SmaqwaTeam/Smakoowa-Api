namespace Smakoowa_Api.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DataContext context) : base(context) { }
    }
}
