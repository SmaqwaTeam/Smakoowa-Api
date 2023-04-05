using Smakoowa_Api.Attributes;
using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _tagService.GetAll();
        }

        [HttpGet("GetById/{tagId}")]
        public async Task<ServiceResponse> GetById(int tagId)
        {
            return await _tagService.GetById(tagId);
        }

        [HttpGet("GetByIds")]
        public async Task<ServiceResponse> GetByIds([FromQuery] int[] tagIds)
        {
            return await _tagService.GetByIds(tagIds.ToList());
        }

        [HttpGet("GetByType")]
        public async Task<ServiceResponse> GetByType(TagType tagType)
        {
            return await _tagService.GetByType(tagType);
        }
    }
}

