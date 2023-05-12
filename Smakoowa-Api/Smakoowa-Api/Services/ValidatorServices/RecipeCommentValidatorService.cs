namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeCommentValidatorService : CommentValidatorService<Recipe>, IRecipeCommentValidatorService
    {
        public RecipeCommentValidatorService(IConfiguration configuration, IApiUserService apiUserService, 
            IBaseRepository<Recipe> commentedRepository) : base(configuration, "RecipeComment", apiUserService, commentedRepository)
        {
        }
    }
}
