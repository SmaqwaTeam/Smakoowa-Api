namespace Smakoowa_Api.Controllers.Tags
{
    [Route("api/Tags")]
    [ApiController]
    public class TagsGetterController : ControllerBase
    {
        private readonly ITagGetterService _tagGetterService;

        public TagsGetterController(ITagGetterService tagGetterService)
        {
            _tagGetterService = tagGetterService;
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _tagGetterService.GetAll();
        }

        [HttpGet("GetById/{tagId}")]
        public async Task<ServiceResponse> GetById(int tagId)
        {
            return await _tagGetterService.GetById(tagId);
        }

        [HttpGet("GetByIds")]
        public async Task<ServiceResponse> GetByIds([FromQuery] int[] tagIds)
        {
            return await _tagGetterService.GetByIds(tagIds.ToList());
        }

        [HttpGet("GetByType")]
        public async Task<ServiceResponse> GetByType(TagType tagType)
        {
            return await _tagGetterService.GetByType(tagType);
        }

        [JwtAuthorize("Admin", "User")]
        [HttpGet("GetUserLikedTags")]
        public async Task<ServiceResponse> GetUserLikedTags()
        {
            return await _tagGetterService.GetUserLikedTags();
        }
    }
}
