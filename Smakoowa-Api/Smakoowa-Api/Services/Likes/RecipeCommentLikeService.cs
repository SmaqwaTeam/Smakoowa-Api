using Smakoowa_Api.Services.Interfaces.Helper;
using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Services.Likes
{
    public class RecipeCommentLikeService : LikeService<RecipeCommentLike>, IRecipeCommentLikeService
    {
        public RecipeCommentLikeService(IBaseRepository<RecipeCommentLike> likeRepository,
            IHelperService<LikeService<RecipeCommentLike>> helperService, IApiUserService apiUserService,
            IRecipeCommentLikeValidatorService likeValidatorService)
            : base(likeRepository, helperService, apiUserService, likeValidatorService, LikeableType.RecipeComment)
        {
        }
    }
}
