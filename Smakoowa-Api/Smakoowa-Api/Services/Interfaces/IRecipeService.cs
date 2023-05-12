namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeService : ICreatorService<RecipeRequestDto>, IEditorService<RecipeRequestDto>, IDeleterService
    {
        public Task<ServiceResponse> GetById(int id);
        public Task<ServiceResponse> GetAll(GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetByIdDetailed(int recipeId);
        public Task<ServiceResponse> GetCurrentUsersRecipes(GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetRecipesByTagIds(List<int> tagIds, GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetRecipesByCategoryId(int categoryId, GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> SearchRecipesByName(string querry, GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetRecipiesByLikedTags(GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetLikedRecipies(GetRecipeParameters? getRecipeParameters);
        public Task<ServiceResponse> GetUserRecipies(int userId, GetRecipeParameters? getRecipeParameters);
    }
}
