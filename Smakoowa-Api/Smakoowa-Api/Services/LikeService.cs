namespace Smakoowa_Api.Services
{
    public abstract class LikeService<T> where T : Like
    {
        protected readonly IHelperService<LikeService<T>> _helperService;
        protected readonly IBaseRepository<T> _likeRepository;
        protected readonly IApiUserService _apiUserService;
        protected readonly ILikeValidatorService _likeValidatorService;
        protected readonly LikeableType _likeableType;

        public LikeService(IBaseRepository<T> likeRepository, IHelperService<LikeService<T>> helperService, IApiUserService apiUserService,
            ILikeValidatorService likeValidatorService, LikeableType likeableType)
        {
            _likeRepository = likeRepository;
            _helperService = helperService;
            _apiUserService = apiUserService;
            _likeValidatorService = likeValidatorService;
            _likeableType = likeableType;
        }

        public async Task<ServiceResponse> RemoveLike(int likedId)
        {
            var likeToRemove = await _likeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedId == likedId
                && c.CreatorId == _apiUserService.GetCurrentUserId());

            if (likeToRemove == null) return ServiceResponse.Error($"Like of item with id: {likedId} not found.");

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


        public async Task<int> GetLikeCount(int likedId)
        {
            return (await _likeRepository.FindByConditions(rl => rl.LikedId == likedId)).Count();
        }

        public async Task<ServiceResponse> AddLike(int likedId)
        {
            var validationResult = await _likeValidatorService.ValidateAddLike(likedId);
            if (!validationResult.SuccessStatus) return validationResult;

            var newLike = new Like { LikedId = likedId, LikeableType = _likeableType };

            try
            {
                await _likeRepository.Create((T)CreateNewLike(likedId, _likeableType));
                return ServiceResponse.Success("Like added.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while adding a like.");
            }
        }

        private Like CreateNewLike(int likedId, LikeableType likeableType)
        {
            switch (likeableType)
            {
                case LikeableType.Recipe:
                    return new RecipeLike { LikeableType = likeableType, LikedId = likedId };
                case LikeableType.Tag:
                    return new TagLike { LikeableType = likeableType, LikedId = likedId };
                case LikeableType.RecipeComment:
                    return new RecipeCommentLike { LikeableType = likeableType, LikedId = likedId };
                case LikeableType.CommentReply:
                    return new CommentReplyLike { LikeableType = likeableType, LikedId = likedId };
                default:
                    return null;
            }
        }
    }
}
