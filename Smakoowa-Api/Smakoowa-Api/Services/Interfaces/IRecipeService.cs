namespace Smakoowa_Api.Services.Interfaces
{
    public interface IRecipeService : ICreatorService<RecipeRequestDto>, IEditorService<RecipeRequestDto>, IDeleterService, IGetterService
    {
        public Task<ServiceResponse> GetByIdDetailed(int recipeId);
        public Task<ServiceResponse> GetCurrentUsersRecipes();
        public Task<ServiceResponse> GetRecipesByTagIds(List<int> tagIds);
        public Task<ServiceResponse> GetRecipesByCategoryId(int categoryId);
        public Task<ServiceResponse> SearchRecipesByName(string querry);
        public Task<ServiceResponse> GetRecipiesByLikedTags();
        public Task<ServiceResponse> GetLikedRecipies();
    }
}
