namespace Smakoowa_Api.Services.ValidatorServices
{
    public class LikeValidatorService : ILikeValidatorService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IBaseRepository<RecipeLike> _recipeLikeRepository;

        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly IBaseRepository<RecipeCommentLike> _recipeCommentLikeRepository;

        private readonly ICommentReplyRepository _commentReplyRepository;
        private readonly IBaseRepository<CommentReplyLike> _commentReplyLikeRepository;

        private readonly IApiUserService _apiUserService;

        public LikeValidatorService(IRecipeRepository recipeRepository, IBaseRepository<RecipeLike> recipeLikeRepository, 
            IRecipeCommentRepository recipeCommentRepository, IBaseRepository<RecipeCommentLike> recipeCommentLikeRepository, 
            ICommentReplyRepository commentReplyRepository, IBaseRepository<CommentReplyLike> commentReplyLikeRepository, IApiUserService apiUserService)
        {
            _recipeRepository = recipeRepository;
            _recipeLikeRepository = recipeLikeRepository;
            _recipeCommentRepository = recipeCommentRepository;
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
            _commentReplyRepository = commentReplyRepository;
            _commentReplyLikeRepository = commentReplyLikeRepository;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> ValidateCommentReplyLike(int commentReplyId)
        {
            if (!await _commentReplyRepository.CheckIfExists(cr => cr.Id == commentReplyId))
            {
                return ServiceResponse.Error($"Comment reply with id: {commentReplyId} does not exist.");
            }

            if (await _commentReplyLikeRepository.CheckIfExists(l => l.CommentReplyId == commentReplyId && l.CreatorId == _apiUserService.GetCurrentUserId().Result))
            {
                return ServiceResponse.Error($"Comment reply with id: {commentReplyId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateRecipeCommentLike(int recipeCommentId)
        {
            if (!await _recipeCommentRepository.CheckIfExists(rc => rc.Id == recipeCommentId))
            {
                return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} does not exist.");
            }

            if (await _recipeCommentLikeRepository.CheckIfExists(l => l.RecipeCommentId == recipeCommentId && l.CreatorId == _apiUserService.GetCurrentUserId().Result))
            {
                return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateRecipeLike(int recipeId)
        {
            if (!await _recipeRepository.CheckIfExists(r => r.Id == recipeId))
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} does not exist.");
            }

            var lik = await _recipeLikeRepository.FindByConditionsFirstOrDefault(l => l.CreatorId == _apiUserService.GetCurrentUserId().Result);
            if (await _recipeLikeRepository.CheckIfExists(l => l.RecipeId == recipeId && l.CreatorId == _apiUserService.GetCurrentUserId().Result))
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }
    }
}
