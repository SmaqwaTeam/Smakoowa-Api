namespace Smakoowa_Api.Services.MapperServices
{
    public class IngredientMapperService : IIngredientMapperService
    {
        private readonly IMapper _mapper;
        public IngredientMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Ingredient> MapCreateIngredientRequestDtos(List<IngredientRequestDto> ingredientRequestDtos, int recipeId)
        {
            List<Ingredient> mappedIngredients = new();
            foreach(var ingredient in ingredientRequestDtos) mappedIngredients.Add(_mapper.Map<Ingredient>(ingredient));
            foreach (Ingredient ingredient in mappedIngredients) ingredient.RecipeId = recipeId;
            return mappedIngredients;
        }
    }
}