namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IRecipeMapperService
    {
        public Task<Recipe> MapCreateRecipeRequestDto(RecipeRequestDto recipeRequestDto);
        public GetRecipeResponseDto MapGetRecipeResponseDto(Recipe recipe);
        public GetDetailedRecipeResponseDto MapGetDetailedRecipeResponseDto(Recipe recipe);
        public Task<Recipe> MapEditRecipeRequestDto(RecipeRequestDto recipeRequestDto, Recipe editedRecipe);
    }
}
