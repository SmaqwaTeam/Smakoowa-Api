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

        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] CreateTagRequestDto model)
        {
            return await _tagService.Create(model);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await _tagService.Delete(id);
        }

        [HttpPut("Edit")]
        public async Task<ServiceResponse> Edit([FromBody] EditTagRequestDto model)
        {
            return await _tagService.Edit(model);
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _tagService.GetAll();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ServiceResponse> GetById(int id)
        {
            return await _tagService.GetById(id);
        }
    }
}

