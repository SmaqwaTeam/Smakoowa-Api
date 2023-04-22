namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeMapperService : IRecipeMapperService
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly IControllerStatisticsService _controllerStatisticsService;
        private readonly IRecipeLikeService _recipeLikeService;

        public RecipeMapperService(IMapper mapper, ITagRepository tagRepository, IControllerStatisticsService controllerStatisticsService,
            IRecipeLikeService recipeLikeService)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _controllerStatisticsService = controllerStatisticsService;
            _recipeLikeService = recipeLikeService;
        }

        public async Task<Recipe> MapCreateRecipeRequestDto(RecipeRequestDto createRecipeRequestDto)
        {
            var mappedRecipe = _mapper.Map<Recipe>(createRecipeRequestDto);
            if (createRecipeRequestDto.TagIds?.Count() > 0)
            {
                var tags = await _tagRepository.FindByConditions(t => createRecipeRequestDto.TagIds.Contains(t.Id));
                mappedRecipe.Tags = tags.ToList();
            }
            return mappedRecipe;
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

        public async Task<RecipeResponseDto> MapGetRecipeResponseDto(Recipe recipe)
        {
            var mappedRecipe = _mapper.Map<RecipeResponseDto>(recipe);
            mappedRecipe = await CompleteRecipeData(mappedRecipe);

            return mappedRecipe;
        }

        public async Task<DetailedRecipeResponseDto> MapGetDetailedRecipeResponseDto(Recipe recipe)
        {
            var mappedRecipe = _mapper.Map<DetailedRecipeResponseDto>(recipe);
            mappedRecipe = (DetailedRecipeResponseDto)await CompleteRecipeData(mappedRecipe);

            return mappedRecipe;
        }

        private async Task<RecipeResponseDto> CompleteRecipeData(RecipeResponseDto mappedRecipe)
        {
            mappedRecipe.ViewCount = await _controllerStatisticsService.GetRecipeViewCount(mappedRecipe.Id);
            mappedRecipe.LikeCount = await _recipeLikeService.GetRecipeLikeCount(mappedRecipe.Id);

            return mappedRecipe;
        }
    }
}