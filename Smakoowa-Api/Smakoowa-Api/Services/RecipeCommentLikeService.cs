namespace Smakoowa_Api.Services
{
    public class RecipeCommentLikeService : LikeService, IRecipeCommentLikeService
    {
        private readonly ILikeValidatorService _likeValidatorService;
        private readonly IBaseRepository<RecipeCommentLike> _recipeCommentLikeRepository;

        public RecipeCommentLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService,
            ILikeValidatorService likeValidatorService, IBaseRepository<RecipeCommentLike> recipeCommentLikeRepository)
            : base(likeRepository, helperService)
        {
            _likeValidatorService = likeValidatorService;
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
        }

        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            var recipeCommentLikeValidationResult = await _likeValidatorService.ValidateRecipeCommentLike(recipeCommentId);
            if (!recipeCommentLikeValidationResult.SuccessStatus) return recipeCommentLikeValidationResult;

            var recipeCommentLike = new RecipeCommentLike { RecipeCommentId = recipeCommentId, LikeableType = LikeableType.RecipeComment };
            return await AddLike(recipeCommentLike);
        }

        public async Task<ServiceResponse> RemoveRecipeCommentLike(int likeId)
        {
            var likeToRemove = await _recipeCommentLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
