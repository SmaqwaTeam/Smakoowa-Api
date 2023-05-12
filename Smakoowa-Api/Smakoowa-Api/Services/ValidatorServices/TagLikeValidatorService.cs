namespace Smakoowa_Api.Services.ValidatorServices
{
    public class TagLikeValidatorService : ITagLikeValidatorService
    {
        private readonly IApiUserService _apiUserService;
        private readonly ITagLikeRepository _likeRepository;
        private readonly ITagRepository _likedItemRepository;

        public TagLikeValidatorService(ITagRepository likedItemRepository, ITagLikeRepository likeRepository, IApiUserService apiUserService)
        {
            _likedItemRepository = likedItemRepository;
            _likeRepository = likeRepository;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> ValidateAddLike(int likedId)
        {
            if (!await _likedItemRepository.CheckIfExists(r => r.Id == likedId))
            {
                return ServiceResponse.Error($"Item with id: {likedId} does not exist.");
            }

            if (await _likeRepository.CheckIfExists(
                l => l.LikedId == likedId
                && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Item with id: {likedId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }
    }
}
