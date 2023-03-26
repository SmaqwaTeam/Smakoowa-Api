using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeMapperService : IRecipeMapperService
    {
        private readonly IMapper _mapper;
        public RecipeMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Recipe MapCreateRecipeRequestDto(RecipeRequestDto createRecipeRequestDto)
        {
            return _mapper.Map<Recipe>(createRecipeRequestDto);
        }

        public GetRecipeResponseDto MapGetRecipeResponseDto(Recipe recipe)
        {
            return _mapper.Map<GetRecipeResponseDto>(recipe);
        }

        public Recipe MapEditRecipeRequestDto(RecipeRequestDto editRecipeRequestDto, Recipe editedRecipe)
        {
            editedRecipe.Name = editRecipeRequestDto.Name;
            editedRecipe.Description = editRecipeRequestDto.Description;
            editedRecipe.ServingsTier = editRecipeRequestDto.ServingsTier;
            editedRecipe.TimeToMakeTier = editRecipeRequestDto.TimeToMakeTier;
            editedRecipe.CategoryId = editRecipeRequestDto.CategoryId;
            return editedRecipe;
        }
    }
}