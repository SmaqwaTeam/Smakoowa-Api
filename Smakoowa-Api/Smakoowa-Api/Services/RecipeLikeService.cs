namespace Smakoowa_Api.Services
{
    public class RecipeLikeService : LikeService, IRecipeLikeService
    {
        private readonly IBaseRepository<RecipeLike> _recipeLikeRepository;

        public RecipeLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService,
            ILikeValidatorService likeValidatorService, IBaseRepository<RecipeLike> recipeLikeRepository)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _recipeLikeRepository = recipeLikeRepository;
        }

        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            var recipeLikeValidationResult = await _likeValidatorService.ValidateRecipeLike(recipeId);
            if (!recipeLikeValidationResult.SuccessStatus) return recipeLikeValidationResult;

            var recipeLike = new RecipeLike { RecipeId = recipeId, LikeableType = LikeableType.Recipe };
            return await AddLike(recipeLike);
        }

        public async Task<ServiceResponse> RemoveRecipeLike(int recipeId)
        {
            var likeToRemove = await _recipeLikeRepository.FindByConditionsFirstOrDefault(c => c.LikedRecipe.Id == recipeId && c.CreatorId == _apiUserService.GetCurrentUserId());
            if (likeToRemove == null) return ServiceResponse.Error($"Like of recipe with id: {recipeId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
