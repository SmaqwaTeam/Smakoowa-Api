namespace Smakoowa_Api.Services
{
    public class TagLikeService : LikeService, ITagLikeService
    {
        private readonly IBaseRepository<TagLike> _tagLikeRepository;

        public TagLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, ILikeValidatorService likeValidatorService,
            IBaseRepository<TagLike> tagLikeRepository, IApiUserService apiUserService)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _tagLikeRepository = tagLikeRepository;
        }

        public async Task<ServiceResponse> AddTagLike(int tagId)
        {
            var tagLikeValidationResult = await _likeValidatorService.ValidateTagLike(tagId);
            if (!tagLikeValidationResult.SuccessStatus) return tagLikeValidationResult;

            var tagLike = new TagLike { TagId = tagId, LikeableType = LikeableType.Tag };
            return await AddLike(tagLike);
        }

        public async Task<ServiceResponse> RemoveTagLike(int tagId)
        {
            var likeToRemove = await _tagLikeRepository.FindByConditionsFirstOrDefault(c => c.LikedTag.Id == tagId && c.CreatorId == _apiUserService.GetCurrentUserId());
            if (likeToRemove == null) return ServiceResponse.Error($"Like of tag with id: {tagId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
