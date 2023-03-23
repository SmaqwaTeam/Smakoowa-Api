using ModernPantryBackend.Repositories;
using Smakoowa_Api.Repositories.Interfaces;

namespace Smakoowa_Api.Repositories
{
    public class CommentReplyRepository : BaseRepository<CommentReply>, ICategoryRepository
    {
        public CommentReplyRepository(DataContext context) : base(context) { }
    }
}
