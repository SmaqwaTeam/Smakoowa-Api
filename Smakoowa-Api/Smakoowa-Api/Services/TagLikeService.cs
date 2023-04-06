namespace Smakoowa_Api.Services
{
    public class TagLikeService : LikeService, ITagLikeService
    {
        private readonly ILikeValidatorService _likeValidatorService;
        private readonly IBaseRepository<TagLike> _tagLikeRepository;

        public TagLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, ILikeValidatorService likeValidatorService,
            IBaseRepository<TagLike> tagLikeRepository)
            : base(likeRepository, helperService)
        {
            _likeValidatorService = likeValidatorService;
            _tagLikeRepository = tagLikeRepository;
        }

        public async Task<ServiceResponse> AddTagLike(int tagId)
        {
            var tagLikeValidationResult = await _likeValidatorService.ValidateTagLike(tagId);
            if (!tagLikeValidationResult.SuccessStatus) return tagLikeValidationResult;

            var tagLike = new TagLike { TagId = tagId, LikeableType = LikeableType.Tag };
            return await AddLike(tagLike);
        }

        public async Task<ServiceResponse> RemoveTagLike(int likeId)
        {
            var likeToRemove = await _tagLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
