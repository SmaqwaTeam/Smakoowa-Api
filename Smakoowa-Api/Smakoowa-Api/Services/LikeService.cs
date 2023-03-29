namespace Smakoowa_Api.Services
{
    public class LikeService : ILikeService
    {
        private readonly IApiUserService _apiUserService;
        private readonly IHelperService<LikeService> _helperService;
        private readonly ILikeRepository _likeRepository;

        private readonly IBaseRepository<RecipeLike> _recipeLikeRepository;
        private readonly IBaseRepository<RecipeCommentLike> _recipeCommentLikeRepository;
        private readonly IBaseRepository<CommentReplyLike> _commentReplyLikeRepository;

        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly ICommentReplyRepository _commentReplyRepository;

        public LikeService(ICommentReplyRepository commentReplyRepository, IRecipeCommentRepository recipeCommentRepository, IRecipeRepository recipeRepository, IBaseRepository<CommentReplyLike> commentReplyLikeRepository, IBaseRepository<RecipeCommentLike> recipeCommentLikeRepository, IBaseRepository<RecipeLike> recipeLikeRepository, ILikeRepository likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService)
        {
            _commentReplyRepository = commentReplyRepository;
            _recipeCommentRepository = recipeCommentRepository;
            _recipeRepository = recipeRepository;
            _commentReplyLikeRepository = commentReplyLikeRepository;
            _recipeCommentLikeRepository = recipeCommentLikeRepository;
            _recipeLikeRepository = recipeLikeRepository;
            _likeRepository = likeRepository;
            _helperService = helperService;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            if (!await _recipeRepository.CheckIfExists(r => r.Id == recipeId))
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} does not exist.");
            }

            if (await _recipeLikeRepository.CheckIfExists(l => l.RecipeId == recipeId && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} is already liked by current user.");
            }

            var recipeLike = new RecipeLike { RecipeId = recipeId, LikeableType = LikeableType.Recipe };
            return await AddLike(recipeLike);
        }

        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            if (!await _recipeCommentRepository.CheckIfExists(rc => rc.Id == recipeCommentId))
            {
                return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} does not exist.");
            }

            if (await _recipeCommentLikeRepository.CheckIfExists(l => l.RecipeCommentId == recipeCommentId && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} is already liked by current user.");
            }

            var recipeCommentLike = new RecipeCommentLike { RecipeCommentId = recipeCommentId, LikeableType = LikeableType.RecipeComment };
            return await AddLike(recipeCommentLike);
        }

        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            if (!await _commentReplyRepository.CheckIfExists(cr => cr.Id == commentReplyId))
            {
                return ServiceResponse.Error($"Comment reply with id: {commentReplyId} does not exist.");
            }

            if (await _commentReplyLikeRepository.CheckIfExists(l => l.CommentReplyId == commentReplyId && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Comment reply with id: {commentReplyId} is already liked by current user.");
            }

            var commentReplyLike = new CommentReplyLike { CommentReplyId = commentReplyId, LikeableType = LikeableType.CommentReply };
            return await AddLike(commentReplyLike);
        }

        private async Task<ServiceResponse> AddLike(Like like)
        {
            try
            {
                await _likeRepository.Create(like);
                return ServiceResponse.Success("Like added.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while adding a like.");
            }
        }

        public async Task<ServiceResponse> RemoveRecipeLike(int likeId)
        {
            var likeToRemove = await _recipeLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }

        public async Task<ServiceResponse> RemoveRecipeCommentLike(int likeId)
        {
            var likeToRemove = await _recipeCommentLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }

        public async Task<ServiceResponse> RemoveCommentReplyLike(int likeId)
        {
            var likeToRemove = await _commentReplyLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }

        private async Task<ServiceResponse> RemoveLike(Like likeToRemove)
        {
            try
            {
                await _likeRepository.Delete(likeToRemove);
                return ServiceResponse.Success("Like removed.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while removing a like.");
            }
        }
    }
}
