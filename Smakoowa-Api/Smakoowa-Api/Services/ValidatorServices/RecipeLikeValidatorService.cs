namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeLikeValidatorService : LikeValidatorService<RecipeLike, Recipe>, IRecipeLikeValidatorService
    {
        public RecipeLikeValidatorService(IBaseRepository<Recipe> likedItemRepository, IBaseRepository<RecipeLike> likeRepository, 
            IApiUserService apiUserService) 
            : base(likedItemRepository, likeRepository, apiUserService)
        {
        }

        //private readonly IApiUserService _apiUserService;
        //private readonly IRecipeLikeRepository _likeRepository;
        //private readonly IRecipeRepository _likedItemRepository;
        //private readonly IBaseRepository<IDbModel> _baseRepository;


        //public async Task<ServiceResponse> ValidateAddLike(int likedId)
        //{
        //    bool a = await _baseRepository.CheckIfExists(r => ((Recipe)r).Id == likedId);

        //    if (!await _likedItemRepository.CheckIfExists(r => r.Id == likedId))
        //    {
        //        return ServiceResponse.Error($"Item with id: {likedId} does not exist.");
        //    }

        //    if (await _likeRepository.CheckIfExists(
        //        l => l.LikedId == likedId
        //        && l.CreatorId == _apiUserService.GetCurrentUserId()))
        //    {
        //        return ServiceResponse.Error($"Item with id: {likedId} is already liked by current user.");
        //    }

        //    return ServiceResponse.Success();
        //}

    }
}
