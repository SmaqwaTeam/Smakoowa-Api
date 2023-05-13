using Smakoowa_Api.Services.Interfaces.Helper;
using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Services.Likes
{
    public class TagLikeService : LikeService<TagLike>, ITagLikeService
    {
        public TagLikeService(IBaseRepository<TagLike> likeRepository, IHelperService<LikeService<TagLike>> helperService,
            IApiUserService apiUserService, ITagLikeValidatorService likeValidatorService)
            : base(likeRepository, helperService, apiUserService, likeValidatorService, LikeableType.Tag)
        {
        }

        public async Task<IEnumerable<TagLike>> GetUserTagLikes()
        {
            return await _likeRepository.FindByConditions(tl => tl.CreatorId == _apiUserService.GetCurrentUserId());
        }
    }
}
