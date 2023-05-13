namespace Smakoowa_Api.Services.Comments
{
    public class RecipeCommentService : CommentService<RecipeComment>, IRecipeCommentService
    {
        public RecipeCommentService(IRecipeCommentMapperService commentMapperService, IRecipeCommentValidatorService commentValidatorService,
            IBaseRepository<RecipeComment> commentRepository, IApiUserService apiUserService,
            IHelperService<CommentService<RecipeComment>> helperService)
            : base(commentMapperService, commentValidatorService, commentRepository, apiUserService, helperService)
        {
        }
    }
}
