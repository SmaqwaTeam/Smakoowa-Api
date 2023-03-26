using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] RecipeRequestDto recipeRequestDto)
        {
            return await _recipeService.Create(recipeRequestDto);
        }

        [HttpDelete("Delete/{recipeId}")]
        public async Task<ServiceResponse> Delete(int recipeId)
        {
            return await _recipeService.Delete(recipeId);
        }

        [HttpPut("Edit/{recipeId}")]
        public async Task<ServiceResponse> Edit([FromBody] RecipeRequestDto recipeRequestDto, int recipeId)
        {
            return await _recipeService.Edit(recipeRequestDto, recipeId);
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _recipeService.GetAll();
        }

        [HttpGet("GetById/{recipeId}")]
        public async Task<ServiceResponse> GetById(int recipeId)
        {
            return await _recipeService.GetById(recipeId);
        }
    }
}
