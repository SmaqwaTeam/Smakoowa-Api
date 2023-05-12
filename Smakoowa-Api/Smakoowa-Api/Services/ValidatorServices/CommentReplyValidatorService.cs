namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CommentReplyValidatorService : CommentValidatorService<RecipeComment>, ICommentReplyValidatorService
    {
        public CommentReplyValidatorService(IConfiguration configuration, IApiUserService apiUserService,
            IBaseRepository<RecipeComment> commentedRepository)
            : base(configuration, "CommentReply", apiUserService, commentedRepository)
        {
        }
    }
}
