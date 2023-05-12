namespace Smakoowa_Api.Services.ValidatorServices
{
    public class TagLikeValidatorService : LikeValidatorService<TagLike, Tag>, ITagLikeValidatorService
    {
        public TagLikeValidatorService(IBaseRepository<Tag> likedItemRepository, IBaseRepository<TagLike> likeRepository,
            IApiUserService apiUserService)
            : base(likedItemRepository, likeRepository, apiUserService)
        {
        }
    }
}
