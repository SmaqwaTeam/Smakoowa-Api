namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IIngredientMapperService
    {
        public List<Ingredient> MapCreateIngredientRequestDtos(List<IngredientRequestDto> ingredientRequestDtos, int recipeId);
    }
}
