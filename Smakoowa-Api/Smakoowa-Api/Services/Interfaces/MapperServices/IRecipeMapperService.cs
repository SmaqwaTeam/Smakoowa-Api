namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IRecipeMapperService
    {
        public Task<Recipe> MapCreateRecipeRequestDto(RecipeRequestDto recipeRequestDto);
        public Task<RecipeResponseDto> MapGetRecipeResponseDto(Recipe recipe);
        public Task<DetailedRecipeResponseDto> MapGetDetailedRecipeResponseDto(Recipe recipe);
        public Task<Recipe> MapEditRecipeRequestDto(RecipeRequestDto recipeRequestDto, Recipe editedRecipe);
    }
}
