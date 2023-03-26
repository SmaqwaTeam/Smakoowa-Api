using Smakoowa_Api.Models.RequestDtos;

namespace Smakoowa_Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeMapperService _recipeMapperService;
        private readonly IRecipeValidatorService _recipeValidatorService;
        private readonly IHelperService<RecipeService> _helperService;
        public RecipeService(IRecipeRepository recipeRepository, IRecipeMapperService recipeMapperService,
    IRecipeValidatorService recipeValidatorService, IHelperService<RecipeService> helperService)
        {
            _recipeRepository = recipeRepository;
            _recipeMapperService = recipeMapperService;
            _recipeValidatorService = recipeValidatorService;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> Create(RecipeRequestDto recipeRequestDto)
        {
            var validationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var recipe = _recipeMapperService.MapCreateRecipeRequestDto(recipeRequestDto);

            try
            {
                var createdRecipe = await _recipeRepository.Create(recipe);
                if (createdRecipe == null) return ServiceResponse.Error("Failed to create recipe.");
                return ServiceResponse.Success("Recipe created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a recipe.");
            }
        }

        public async Task<ServiceResponse> Delete(int recipeId)
        {
            var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");

            try
            {
                await _recipeRepository.Delete(recipe);
                return ServiceResponse.Success("Recipe deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a recipe.");
            }
        }

        public async Task<ServiceResponse> Edit(RecipeRequestDto recipeRequestDto, int recipeId)
        {
            var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");

            var validationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);

            var updatedRecipe = _recipeMapperService.MapEditRecipeRequestDto(recipeRequestDto, recipe);

            try
            {
                await _recipeRepository.Edit(updatedRecipe);
                return ServiceResponse.Success("Recipe edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a recipe.");
            }
        }

        public async Task<ServiceResponse> GetAll()
        {
            try
            {
                var recipes = await _recipeRepository.FindAll();
                var getRecipesResponseDto = new List<GetRecipeResponseDto>();
                foreach (Recipe recipe in recipes) getRecipesResponseDto.Add(_recipeMapperService.MapGetRecipeResponseDto(recipe));
                return ServiceResponse<List<GetRecipeResponseDto>>.Success(getRecipesResponseDto, "Recipes retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipes.");
            }
        }

        public async Task<ServiceResponse> GetById(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
                if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");
                var getRecipeResponseDto = _recipeMapperService.MapGetRecipeResponseDto(recipe);
                return ServiceResponse<GetRecipeResponseDto>.Success(getRecipeResponseDto, "Recipe retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipe.");
            }
        }
    }
}



