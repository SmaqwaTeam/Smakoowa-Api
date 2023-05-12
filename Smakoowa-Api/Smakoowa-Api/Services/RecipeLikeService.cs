namespace Smakoowa_Api.Services
{
    public class RecipeLikeService : LikeService, IRecipeLikeService
    {
        private readonly IRecipeLikeRepository _recipeLikeRepository;

        public RecipeLikeService(ILikeRepository<Like> likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService,
            IRecipeLikeValidatorService likeValidatorService, IRecipeLikeRepository recipeLikeRepository)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _recipeLikeRepository = recipeLikeRepository;
        }

        public async Task<ServiceResponse> AddLike(int recipeId)
        {
            var recipeLikeValidationResult = await _likeValidatorService.ValidateAddLike(recipeId);
            if (!recipeLikeValidationResult.SuccessStatus) return recipeLikeValidationResult;

            var recipeLike = new RecipeLike { LikedId = recipeId, LikeableType = LikeableType.Recipe };
            return await AddLike(recipeLike);
        }

        public async Task<int> GetLikeCount(int recipeId)
        {
            return (await _recipeLikeRepository.FindByConditions(rl => rl.LikedId == recipeId)).Count();
        }

        public async Task<ServiceResponse> RemoveLike(int recipeId)
        {
            var likeToRemove = await _recipeLikeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedRecipe.Id == recipeId
                && c.CreatorId == _apiUserService.GetCurrentUserId());

            if (likeToRemove == null) return ServiceResponse.Error($"Like of recipe with id: {recipeId} not found.");

            return await RemoveLike(likeToRemove);
        }
    }
}
