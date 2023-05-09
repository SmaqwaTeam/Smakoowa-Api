namespace Smakoowa_Api.Services.ValidatorServices
{
    public class LikeValidatorService : ILikeValidatorService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeLikeRepository _recipeLikeRepository;
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly IRecipeCommentLikeRepository _recipeCommentLikeRepository;
        private readonly ICommentReplyRepository _commentReplyRepository;
        private readonly ICommentReplyLikeRepository _commentReplyLikeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagLikeRepository _tagLikeRepository;
        private readonly IApiUserService _apiUserService;

        public LikeValidatorService(IRecipeRepository recipeRepository, IRecipeLikeRepository recipeLikeRepository,
            IRecipeCommentRepository recipeCommentRepository, IRecipeCommentLikeRepository recipeCommentLikeRepository,
            ICommentReplyRepository commentReplyRepository, ICommentReplyLikeRepository commentReplyLikeRepository, 
            IApiUserService apiUserService, ITagRepository tagRepository, ITagLikeRepository tagLikeRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeLikeRepository = recipeLikeRepository;
            _recipeCommentRepository = recipeCommentRepository;
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
            _commentReplyRepository = commentReplyRepository;
            _commentReplyLikeRepository = commentReplyLikeRepository;
            _apiUserService = apiUserService;
            _tagRepository = tagRepository;
            _tagLikeRepository = tagLikeRepository;
        }

        public async Task<ServiceResponse> ValidateCommentReplyLike(int commentReplyId)
        {
            if (!await _commentReplyRepository.CheckIfExists(cr => cr.Id == commentReplyId))
            {
                return ServiceResponse.Error($"Comment reply with id: {commentReplyId} does not exist.");
            }

            if (await _commentReplyLikeRepository.CheckIfExists(l => l.CommentReplyId == commentReplyId && l.CreatorId == _apiUserService.GetCurrentUserId()))
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

            if (await _recipeCommentLikeRepository.CheckIfExists(l => l.RecipeCommentId == recipeCommentId && l.CreatorId == _apiUserService.GetCurrentUserId()))
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

            if (await _recipeLikeRepository.CheckIfExists(l => l.RecipeId == recipeId && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> ValidateTagLike(int tagId)
        {
            if (!await _tagRepository.CheckIfExists(r => r.Id == tagId))
            {
                return ServiceResponse.Error($"Tag with id: {tagId} does not exist.");
            }

            if (await _tagLikeRepository.CheckIfExists(l => l.TagId == tagId && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Tag with id: {tagId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }
    }
}
