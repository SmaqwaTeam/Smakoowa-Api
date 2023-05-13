using Smakoowa_Api.Services.Interfaces.Helper;

namespace Smakoowa_Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeMapperService _recipeMapperService;
        private readonly IRecipeValidatorService _recipeValidatorService;
        private readonly IHelperService<RecipeService> _helperService;
        private readonly IApiUserService _apiUserService;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeMapperService recipeMapperService,
            IRecipeValidatorService recipeValidatorService, IHelperService<RecipeService> helperService, IApiUserService apiUserService)
        {
            _recipeRepository = recipeRepository;
            _recipeMapperService = recipeMapperService;
            _recipeValidatorService = recipeValidatorService;
            _helperService = helperService;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> Create(RecipeRequestDto recipeRequestDto)
        {
            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return recipeValidationResult;

            var recipe = await _recipeMapperService.MapCreateRecipeRequestDto(recipeRequestDto);

            try
            {
                await _recipeRepository.Create(recipe);
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
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.", HttpStatusCode.NotFound);

            if (recipe.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.", HttpStatusCode.Unauthorized);

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
            if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.", HttpStatusCode.NotFound);

            if (recipe.CreatorId != _apiUserService.GetCurrentUserId())
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.", HttpStatusCode.Unauthorized);

            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return ServiceResponse.Error(recipeValidationResult.Message);

            var updatedRecipe = await _recipeMapperService.MapEditRecipeRequestDto(recipeRequestDto, recipe);

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
    }
}