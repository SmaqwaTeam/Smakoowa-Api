using Smakoowa_Api.Models.RequestDtos;

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
        public async Task<ServiceResponse> Create([FromBody] CategoryRequestDto categoryRequestDto)
        {
            return await _categoryService.Create(categoryRequestDto);
        }

        [HttpDelete("Delete/{categoryId}")]
        public async Task<ServiceResponse> Delete(int categoryId)
        {
            return await _categoryService.Delete(categoryId);
        }

        [HttpPut("Edit/{categoryId}")]
        public async Task<ServiceResponse> Edit([FromBody] CategoryRequestDto categoryRequestDto, int categoryId)
        {
            return await _categoryService.Edit(categoryRequestDto, categoryId);
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet("GetById/{categoryId}")]
        public async Task<ServiceResponse> GetById(int categoryId)
        {
            return await _categoryService.GetById(categoryId);
        }
    }
}
