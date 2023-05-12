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

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetAll(getRecipeParameters);
        }

        [HttpGet("GetById/{recipeId}")]
        public async Task<ServiceResponse> GetById(int recipeId)
        {
            return await _recipeService.GetById(recipeId);
        }

        [HttpGet("GetByIdDetailed/{recipeId}")]
        public async Task<ServiceResponse> GetByIdDetailed(int recipeId)
        {
            return await _recipeService.GetByIdDetailed(recipeId);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetCurrentUsersRecipes")]
        public async Task<ServiceResponse> GetCurrentUsersRecipes([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetCurrentUsersRecipes(getRecipeParameters);
        }

        [HttpGet("GetRecipesByTagIds")]
        public async Task<ServiceResponse> GetRecipesByTagIds([FromQuery] int[] tagIds, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetRecipesByTagIds(tagIds.ToList(), getRecipeParameters);
        }

        [HttpGet("GetRecipesByCategoryId")]
        public async Task<ServiceResponse> GetRecipesByCategoryId(int categoryId, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetRecipesByCategoryId(categoryId, getRecipeParameters);
        }

        [HttpGet("SearchRecipesByName")]
        public async Task<ServiceResponse> SearchRecipesByName(string querry, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.SearchRecipesByName(querry, getRecipeParameters);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetRecipiesByLikedTags")]
        public async Task<ServiceResponse> GetRecipiesByLikedTags([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetRecipiesByLikedTags(getRecipeParameters);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetLikedRecipies")]
        public async Task<ServiceResponse> GetLikedRecipies([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetLikedRecipies(getRecipeParameters);
        }

        [HttpGet("GetUserRecipies/{userId}")]
        public async Task<ServiceResponse> GetUserRecipies(int userId, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeService.GetUserRecipies(userId, getRecipeParameters);
        }
    }
}
