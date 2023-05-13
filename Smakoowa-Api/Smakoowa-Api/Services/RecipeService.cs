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
            var validationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var mappedNewRecipe = await _recipeMapperService.MapCreateRecipeRequestDto(recipeRequestDto);

            try
            {
                await _recipeRepository.Create(mappedNewRecipe);
                return ServiceResponse.Success("Recipe created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a recipe.");
            }
        }

        public async Task<ServiceResponse> Delete(int recipeId)
        {
            var recipeToDelete = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipeToDelete == null)
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} not found.", HttpStatusCode.NotFound);
            }

            if (recipeToDelete.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
            {
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.", HttpStatusCode.Unauthorized);
            }

            try
            {
                await _recipeRepository.Delete(recipeToDelete);
                return ServiceResponse.Success("Recipe deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a recipe.");
            }
        }

        public async Task<ServiceResponse> Edit(RecipeRequestDto recipeRequestDto, int recipeId)
        {
            var recipeToEdit = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
            if (recipeToEdit == null)
            {
                return ServiceResponse.Error($"Recipe with id: {recipeId} not found.", HttpStatusCode.NotFound);
            }

            if (recipeToEdit.CreatorId != _apiUserService.GetCurrentUserId())
            {
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.", HttpStatusCode.Unauthorized);
            }

            var validationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!validationResult.SuccessStatus)
            {
                return ServiceResponse.Error(validationResult.Message);
            }

            var mappedRecipeToEdit = await _recipeMapperService.MapEditRecipeRequestDto(recipeRequestDto, recipeToEdit);

            try
            {
                await _recipeRepository.Edit(mappedRecipeToEdit);
                return ServiceResponse.Success("Recipe edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a recipe.");
            }
        }
    }
}