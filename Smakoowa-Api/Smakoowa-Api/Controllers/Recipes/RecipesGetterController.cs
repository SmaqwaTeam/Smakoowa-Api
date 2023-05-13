namespace Smakoowa_Api.Controllers.Recipes
{
    [Route("api/Recipes")]
    [ApiController]
    public class RecipesGetterController : ControllerBase
    {
        private readonly IRecipeGetterService _recipeGetterService;

        public RecipesGetterController(IRecipeGetterService recipeGetterService)
        {
            _recipeGetterService = recipeGetterService;
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse> GetAll([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetAll(getRecipeParameters);
        }

        [HttpGet("GetById/{recipeId}")]
        public async Task<ServiceResponse> GetById(int recipeId)
        {
            return await _recipeGetterService.GetById(recipeId);
        }

        [HttpGet("GetByIdDetailed/{recipeId}")]
        public async Task<ServiceResponse> GetByIdDetailed(int recipeId)
        {
            return await _recipeGetterService.GetByIdDetailed(recipeId);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetCurrentUsersRecipes")]
        public async Task<ServiceResponse> GetCurrentUsersRecipes([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetCurrentUsersRecipes(getRecipeParameters);
        }

        [HttpGet("GetRecipesByTagIds")]
        public async Task<ServiceResponse> GetRecipesByTagIds([FromQuery] int[] tagIds, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetRecipesByTagIds(tagIds.ToList(), getRecipeParameters);
        }

        [HttpGet("GetRecipesByCategoryId")]
        public async Task<ServiceResponse> GetRecipesByCategoryId(int categoryId, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetRecipesByCategoryId(categoryId, getRecipeParameters);
        }

        [HttpGet("SearchRecipesByName")]
        public async Task<ServiceResponse> SearchRecipesByName(string querry, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.SearchRecipesByName(querry, getRecipeParameters);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetRecipiesByLikedTags")]
        public async Task<ServiceResponse> GetRecipiesByLikedTags([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetRecipiesByLikedTags(getRecipeParameters);
        }

        [JwtAuthorize("User", "Admin")]
        [HttpGet("GetLikedRecipies")]
        public async Task<ServiceResponse> GetLikedRecipies([FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetLikedRecipies(getRecipeParameters);
        }

        [HttpGet("GetUserRecipies/{userId}")]
        public async Task<ServiceResponse> GetUserRecipies(int userId, [FromQuery] GetRecipeParameters? getRecipeParameters)
        {
            return await _recipeGetterService.GetUserRecipies(userId, getRecipeParameters);
        }
    }
}
