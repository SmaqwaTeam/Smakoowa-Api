namespace Smakoowa_Api.Controllers.Recipes
{
    [Route("api/Recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [JwtAuthorize("User", "Admin")]
        [HttpPost("Create")]
        public async Task<ServiceResponse> Create([FromBody] RecipeRequestDto recipeRequestDto)
        {
            return await _recipeService.Create(recipeRequestDto);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpDelete("Delete/{recipeId}")]
        public async Task<ServiceResponse> Delete(int recipeId)
        {
            return await _recipeService.Delete(recipeId);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpPut("Edit/{recipeId}")]
        public async Task<ServiceResponse> Edit([FromBody] RecipeRequestDto recipeRequestDto, int recipeId)
        {
            return await _recipeService.Edit(recipeRequestDto, recipeId);
        }
    }
}
