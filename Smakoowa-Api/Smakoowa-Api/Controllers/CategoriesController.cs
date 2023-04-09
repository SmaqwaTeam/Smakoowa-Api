using Microsoft.AspNetCore.Mvc.Controllers;
using Smakoowa_Api.Attributes;
using Smakoowa_Api.Events;
using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IRequestCounterService requestCounterService;

        public CategoriesController(ICategoryService categoryService, IBackgroundTaskQueue backgroundTaskQueue, ILogger<CategoriesController> logger, IRequestCounterService requestCounterService)
        {
            _categoryService = categoryService;
            this.backgroundTaskQueue = backgroundTaskQueue;
            _logger = logger;
            this.requestCounterService = requestCounterService;



        }

        [JwtAuthorize("Admin")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] CategoryRequestDto categoryRequestDto)
        {
            return await _categoryService.Create(categoryRequestDto);
        }

        [JwtAuthorize("Admin")]
        [HttpDelete("Delete/{categoryId}")]
        public async Task<ServiceResponse> Delete(int categoryId)
        {
            return await _categoryService.Delete(categoryId);
        }

        [JwtAuthorize("Admin")]
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
            //await backgroundTaskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
            return await _categoryService.GetById(categoryId);
        }

        [HttpGet("GetByIds")]
        public async Task<ServiceResponse> GetByIds([FromQuery] int[] categoryIds)
        {
            return await _categoryService.GetByIds(categoryIds.ToList());
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            _logger.LogInformation("123");
            await requestCounterService.LogRequestCount("Category", "GetById", "1");
        }
    }
}
