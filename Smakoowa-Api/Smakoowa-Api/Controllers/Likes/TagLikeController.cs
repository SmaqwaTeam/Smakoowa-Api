using Smakoowa_Api.Services.Interfaces.Likes;

namespace Smakoowa_Api.Controllers.Likes
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/Likes")]
    [ApiController]
    public class TagLikeController : ControllerBase
    {
        private readonly ITagLikeService _tagLikeService;

        public TagLikeController(ITagLikeService tagLikeService)
        {
            _tagLikeService = tagLikeService;
        }

        [HttpPost("AddTagLike/{tagId}")]
        public async Task<ServiceResponse> AddTagLike(int tagId)
        {
            return await _tagLikeService.AddLike(tagId);
        }

        [HttpDelete("RemoveTagLike/{tagId}")]
        public async Task<ServiceResponse> RemoveTagLike(int tagId)
        {
            return await _tagLikeService.RemoveLike(tagId);
        }
    }
}
