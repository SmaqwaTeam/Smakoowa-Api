namespace Smakoowa_Api.Services
{
    public class RecipeLikeService : LikeService, IRecipeLikeService
    {
        private readonly ILikeValidatorService _likeValidatorService;
        private readonly IBaseRepository<RecipeLike> _recipeLikeRepository;

        public RecipeLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, ILikeValidatorService likeValidatorService, 
            IBaseRepository<RecipeLike> recipeLikeRepository)
            : base(likeRepository, helperService)
        {
            _likeValidatorService = likeValidatorService;
            _recipeLikeRepository = recipeLikeRepository;
        }

        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            var recipeLikeValidationResult = await _likeValidatorService.ValidateRecipeLike(recipeId);
            if (!recipeLikeValidationResult.SuccessStatus) return recipeLikeValidationResult;

            var recipeLike = new RecipeLike { RecipeId = recipeId, LikeableType = LikeableType.Recipe };
            return await AddLike(recipeLike);
        }

        public async Task<ServiceResponse> RemoveRecipeLike(int likeId)
        {
            var likeToRemove = await _recipeLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
