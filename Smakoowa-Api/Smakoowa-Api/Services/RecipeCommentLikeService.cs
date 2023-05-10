namespace Smakoowa_Api.Services
{
    public class RecipeCommentLikeService : LikeService, IRecipeCommentLikeService
    {
        private readonly IRecipeCommentLikeRepository _recipeCommentLikeRepository;

        public RecipeCommentLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService,
            ILikeValidatorService likeValidatorService, IRecipeCommentLikeRepository recipeCommentLikeRepository)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
        }

        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            var recipeCommentLikeValidationResult = await _likeValidatorService.ValidateRecipeCommentLike(recipeCommentId);
            if (!recipeCommentLikeValidationResult.SuccessStatus) return recipeCommentLikeValidationResult;

            var recipeCommentLike = new RecipeCommentLike { RecipeCommentId = recipeCommentId, LikeableType = LikeableType.RecipeComment };
            return await AddLike(recipeCommentLike);
        }

        public async Task<ServiceResponse> RemoveRecipeCommentLike(int recipeCommentId)
        {
            var likeToRemove = await _recipeCommentLikeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedRecipeComment.Id == recipeCommentId
                && c.CreatorId == _apiUserService.GetCurrentUserId());
            if (likeToRemove == null) return ServiceResponse.Error($"Like of recipe comment with id: {recipeCommentId} not found.");

            return await RemoveLike(likeToRemove);
        }

        public async Task<int> GetRecipeCommentLikeCount(int recipeCommentId)
        {
            return (await _recipeCommentLikeRepository.FindByConditions(rcl => rcl.RecipeCommentId == recipeCommentId)).Count();
        }
    }
}
