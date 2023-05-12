using ModernPantryBackend.Interfaces;

namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class LikeValidatorService
    {
        //private readonly IApiUserService _apiUserService;
        //private readonly ILikeRepository<Like> _likeRepository;
        //private readonly IBaseRepository<ILikeable> _likedItemRepository;

        //protected LikeValidatorService(IRepository likeRepository, IApiUserService apiUserService,
        //    IRepository likedItemRepository)
        //{
        //    _likeRepository = (ILikeRepository<Like>)likeRepository;
        //    _apiUserService = apiUserService;
        //    _likedItemRepository = (IBaseRepository<ILikeable>)likedItemRepository;
        //}

        //public async Task<ServiceResponse> ValidateLike(int likedId)
        //{
        //    if (await CheckIfLikedItemExists(likedId))
        //    {
        //        return ServiceResponse.Error($"Item with id: {likedId} does not exist.");
        //    }

        //    if (await CheckIfLikeExists(likedId))
        //    {
        //        return ServiceResponse.Error($"Item with id: {likedId} is already liked by current user.");
        //    }

        //    return ServiceResponse.Success();
        //}

        //private async Task<bool> CheckIfLikedItemExists(int likedId)
        //{
        //    return !await _likedItemRepository.CheckIfExists(r => r.Id == likedId);
        //}

        //private async Task<bool> CheckIfLikeExists(int likedId)
        //{
        //    return await _likeRepository.CheckIfExists(
        //        l => l.LikedId == likedId
        //        && l.CreatorId == _apiUserService.GetCurrentUserId());
        //}
    }
}
