namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeCommentLikeValidatorService : LikeValidatorService<RecipeCommentLike, RecipeComment>, IRecipeCommentLikeValidatorService
    {
        public RecipeCommentLikeValidatorService(IBaseRepository<RecipeComment> likedItemRepository,
            IBaseRepository<RecipeCommentLike> likeRepository, IApiUserService apiUserService)
            : base(likedItemRepository, likeRepository, apiUserService)
        {
        }
    }
}
