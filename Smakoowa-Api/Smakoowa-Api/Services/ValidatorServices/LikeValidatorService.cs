namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class LikeValidatorService<T, C> where C : IDbModel where T : ILike
    {
        protected readonly IApiUserService _apiUserService;
        protected readonly IBaseRepository<T> _likeRepository;
        protected readonly IBaseRepository<C> _likedItemRepository;

        protected LikeValidatorService(IBaseRepository<C> likedItemRepository, IBaseRepository<T> likeRepository, IApiUserService apiUserService)
        {
            _likedItemRepository = likedItemRepository;
            _likeRepository = likeRepository;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> ValidateAddLike(int likedId)
        {
            if (!await _likedItemRepository.CheckIfExists(r => r.Id == likedId))
            {
                return ServiceResponse.Error($"Item with id: {likedId} does not exist.", HttpStatusCode.NotFound);
            }

            if (await _likeRepository.CheckIfExists(
                l => l.LikedId == likedId
                && l.CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Item with id: {likedId} is already liked by current user.", HttpStatusCode.Conflict);
            }

            return ServiceResponse.Success();
        }
    }
}
