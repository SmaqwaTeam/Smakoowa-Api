namespace Smakoowa_Api.Repositories
{
    public class ApiUserRepository : BaseRepository<ApiUser>, IApiUserRepository
    {
        public ApiUserRepository(DataContext context) : base(context) { }

        public override async Task<IEnumerable<ApiUser>> FindAll()
        {
            return await _context.Set<ApiUser>()
            .Include(i => i.Recipes)
            .Include(i => i.RecipeComments)
            .Include(i => i.CommentReplies)
            .Include(i => i.RecipeLikes)
            .Include(i => i.RecipeCommentLikes)
            .Include(i => i.CommentReplyLikes)
            .Include(i => i.TagLikes)
            .ToListAsync();
        }

        public override async Task<IEnumerable<ApiUser>> FindByConditions(Expression<Func<ApiUser, bool>> expression)
        {
            return await _context.Set<ApiUser>()
            .Where(expression)
            .Include(i => i.Recipes)
            .Include(i => i.RecipeComments)
            .Include(i => i.CommentReplies)
            .Include(i => i.RecipeLikes)
            .Include(i => i.RecipeCommentLikes)
            .Include(i => i.CommentReplyLikes)
            .Include(i => i.TagLikes)
            .ToListAsync();
        }

        public override async Task<ApiUser> FindByConditionsFirstOrDefault(Expression<Func<ApiUser, bool>> expresion)
        {
            return await _context.Set<ApiUser>().Where(expresion)
            .Include(i => i.Recipes)
            .Include(i => i.RecipeComments)
            .Include(i => i.CommentReplies)
            .Include(i => i.RecipeLikes)
            .Include(i => i.RecipeCommentLikes)
            .Include(i => i.CommentReplyLikes)
            .Include(i => i.TagLikes)
            .FirstOrDefaultAsync();
        }
    }
}