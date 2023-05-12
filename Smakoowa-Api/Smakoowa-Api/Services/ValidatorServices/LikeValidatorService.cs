using ModernPantryBackend.Interfaces;
using ModernPantryBackend.Repositories;

namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class LikeValidatorService<T, C> where C : IDbModel where T : ILike
    {
        private readonly IApiUserService _apiUserService;
        private readonly IBaseRepository<T> _likeRepository;
        private readonly IBaseRepository<C> _likedItemRepository;

        protected LikeValidatorService(IBaseRepository<C> likedItemRepository, IBaseRepository<T> likeRepository, IApiUserService apiUserService)
        {
            _likedItemRepository = likedItemRepository;
            _likeRepository = likeRepository;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> ValidateAddLike(int likedId)
        {
            bool a = await _likedItemRepository.CheckIfExists(r => ((C)r).Id == likedId);

            if (!await _likedItemRepository.CheckIfExists(r => ((C)r).Id == likedId))
            {
                return ServiceResponse.Error($"Item with id: {likedId} does not exist.");
            }

            if (await _likeRepository.CheckIfExists(
                l => ((T)l).LikedId == likedId
                && ((T)l).CreatorId == _apiUserService.GetCurrentUserId()))
            {
                return ServiceResponse.Error($"Item with id: {likedId} is already liked by current user.");
            }

            return ServiceResponse.Success();
        }
    }
}
