namespace Smakoowa_Api.Services
{
    public class TagLikeService : LikeService, ITagLikeService
    {
        private readonly ITagLikeRepository _tagLikeRepository;

        public TagLikeService(ILikeRepository<Like> likeRepository, IHelperService<LikeService> helperService, ITagLikeValidatorService likeValidatorService,
            ITagLikeRepository tagLikeRepository, IApiUserService apiUserService)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _tagLikeRepository = tagLikeRepository;
        }

        public async Task<ServiceResponse> AddLike(int tagId)
        {
            var tagLikeValidationResult = await _likeValidatorService.ValidateAddLike(tagId);
            if (!tagLikeValidationResult.SuccessStatus) return tagLikeValidationResult;

            var tagLike = new TagLike { LikedId = tagId, LikeableType = LikeableType.Tag };
            return await AddLike(tagLike);
        }

        public async Task<IEnumerable<TagLike>> GetUserTagLikes()
        {
            return await _tagLikeRepository.FindByConditions(tl => tl.CreatorId == _apiUserService.GetCurrentUserId());
        }

        public async Task<ServiceResponse> RemoveLike(int tagId)
        {
            var likeToRemove = await _tagLikeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedTag.Id == tagId
                && c.CreatorId == _apiUserService.GetCurrentUserId());
            if (likeToRemove == null) return ServiceResponse.Error($"Like of tag with id: {tagId} not found.");

            return await RemoveLike(likeToRemove);
        }

        public async Task<int> GetLikeCount(int tagId)
        {
            return (await _tagLikeRepository.FindByConditions(rl => rl.LikedId == tagId)).Count();
        }
    }
}
