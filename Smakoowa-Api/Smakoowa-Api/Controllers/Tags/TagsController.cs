namespace Smakoowa_Api.Controllers.Tags
{
    [Route("api/Tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [JwtAuthorize("Admin")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] TagRequestDto tagRequestDto)
        {
            return await _tagService.Create(tagRequestDto);
        }

        [JwtAuthorize("Admin")]
        [HttpDelete("Delete/{tagId}")]
        public async Task<ServiceResponse> Delete(int tagId)
        {
            return await _tagService.Delete(tagId);
        }

        [JwtAuthorize("Admin")]
        [HttpPut("Edit/{tagId}")]
        public async Task<ServiceResponse> Edit([FromBody] TagRequestDto tagRequestDto, int tagId)
        {
            return await _tagService.Edit(tagRequestDto, tagId);
        }
    }
}

