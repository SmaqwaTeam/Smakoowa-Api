using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeMapperService : IRecipeMapperService
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        public RecipeMapperService(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<Recipe> MapCreateRecipeRequestDto(RecipeRequestDto createRecipeRequestDto)
        {
            var mappedRecipe = _mapper.Map<Recipe>(createRecipeRequestDto);
            if(createRecipeRequestDto.TagIds?.Count() > 0)
            {
                var tags = await _tagRepository.FindByConditions(t => createRecipeRequestDto.TagIds.Contains(t.Id));
                mappedRecipe.Tags = tags.ToList();
            }
            return mappedRecipe;
        }

        public RecipeResponseDto MapGetRecipeResponseDto(Recipe recipe)
        {
            return _mapper.Map<RecipeResponseDto>(recipe);
        }

        public async Task<Recipe> MapEditRecipeRequestDto(RecipeRequestDto editRecipeRequestDto, Recipe editedRecipe)
        {
            editedRecipe.Name = editRecipeRequestDto.Name;
            editedRecipe.Description = editRecipeRequestDto.Description;
            editedRecipe.ServingsTier = editRecipeRequestDto.ServingsTier;
            editedRecipe.TimeToMakeTier = editRecipeRequestDto.TimeToMakeTier;
            editedRecipe.CategoryId = editRecipeRequestDto.CategoryId;

            if (editRecipeRequestDto.TagIds?.Count() > 0)
            {
                var tags = await _tagRepository.FindByConditions(t => editRecipeRequestDto.TagIds.Contains(t.Id));
                editedRecipe.Tags = tags.ToList();
            }
            else editedRecipe.Tags = null;

            return editedRecipe;
        }

        public DetailedRecipeResponseDto MapGetDetailedRecipeResponseDto(Recipe recipe)
        {
            return _mapper.Map<DetailedRecipeResponseDto>(recipe);
        }
    }
}