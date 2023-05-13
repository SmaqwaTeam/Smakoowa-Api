using Smakoowa_Api.Services.Interfaces.Helper;
using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Services.Likes
{
    public class RecipeLikeService : LikeService<RecipeLike>, IRecipeLikeService
    {
        public RecipeLikeService(IBaseRepository<RecipeLike> likeRepository, IHelperService<LikeService<RecipeLike>> helperService,
            IApiUserService apiUserService, IRecipeLikeValidatorService likeValidatorService)
            : base(likeRepository, helperService, apiUserService, likeValidatorService, LikeableType.Recipe)
        {
        }
    }
}
