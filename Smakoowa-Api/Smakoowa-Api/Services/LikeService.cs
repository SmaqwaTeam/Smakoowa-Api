namespace Smakoowa_Api.Services
{
    public class LikeService
    {
        private readonly IHelperService<LikeService> _helperService;
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService)
        {
            _likeRepository = likeRepository;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> AddLike(Like like)
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

        public async Task<ServiceResponse> RemoveLike(Like likeToRemove)
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
