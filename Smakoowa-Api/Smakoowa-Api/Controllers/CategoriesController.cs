namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] CreateCategoryRequestDto model)
        {
            return await _categoryService.Create(model);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            return await _categoryService.Delete(id);
        }

        [HttpPut("Edit")]
        public async Task<ServiceResponse> Edit([FromBody] EditCategoryRequestDto model)
        {
            return await _categoryService.Edit(model);
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ServiceResponse> GetById(int id)
        {
            return await _categoryService.GetById(id);
        }
    }
}
