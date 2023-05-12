namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeLikeValidatorService : LikeValidatorService<RecipeLike, Recipe>, IRecipeLikeValidatorService
    {
        public RecipeLikeValidatorService(IBaseRepository<Recipe> likedItemRepository, IBaseRepository<RecipeLike> likeRepository,
            IApiUserService apiUserService)
            : base(likedItemRepository, likeRepository, apiUserService)
        {
        }
    }
}
