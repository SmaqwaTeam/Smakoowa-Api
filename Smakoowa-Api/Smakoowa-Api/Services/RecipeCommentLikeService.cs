namespace Smakoowa_Api.Services
{
    public class RecipeCommentLikeService : LikeService, IRecipeCommentLikeService
    {
        private readonly IRecipeCommentLikeRepository _recipeCommentLikeRepository;

        public RecipeCommentLikeService(ILikeRepository<Like> likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService,
            IRecipeCommentLikeValidatorService likeValidatorService, IRecipeCommentLikeRepository recipeCommentLikeRepository)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
        }

        public async Task<ServiceResponse> AddLike(int recipeCommentId)
        {
            var recipeCommentLikeValidationResult = await _likeValidatorService.ValidateAddLike(recipeCommentId);
            if (!recipeCommentLikeValidationResult.SuccessStatus) return recipeCommentLikeValidationResult;

            var recipeCommentLike = new RecipeCommentLike { LikedId = recipeCommentId, LikeableType = LikeableType.RecipeComment };
            return await AddLike(recipeCommentLike);
        }

        public async Task<ServiceResponse> RemoveLike(int recipeCommentId)
        {
            var likeToRemove = await _recipeCommentLikeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedRecipeComment.Id == recipeCommentId
                && c.CreatorId == _apiUserService.GetCurrentUserId());
            if (likeToRemove == null) return ServiceResponse.Error($"Like of recipe comment with id: {recipeCommentId} not found.");

            return await RemoveLike(likeToRemove);
        }

        public async Task<int> GetLikeCount(int recipeCommentId)
        {
            return (await _recipeCommentLikeRepository.FindByConditions(rcl => rcl.LikedId == recipeCommentId)).Count();
        }
    }
}
