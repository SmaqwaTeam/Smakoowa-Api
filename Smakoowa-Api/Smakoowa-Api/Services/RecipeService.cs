namespace Smakoowa_Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeMapperService _recipeMapperService;
        private readonly IRecipeValidatorService _recipeValidatorService;
        private readonly IHelperService<RecipeService> _helperService;
        private readonly IIngredientValidatorService _ingredientValidatorService;
        private readonly IIngredientMapperService _ingredientMapperService;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeMapperService recipeMapperService, IRecipeValidatorService recipeValidatorService,
            IHelperService<RecipeService> helperService, IIngredientValidatorService ingredientValidatorService, IIngredientMapperService ingredientMapperService)
        {
            _recipeRepository = recipeRepository;
            _recipeMapperService = recipeMapperService;
            _recipeValidatorService = recipeValidatorService;
            _helperService = helperService;
            _ingredientValidatorService = ingredientValidatorService;
            _ingredientMapperService = ingredientMapperService;
        }

        public async Task<ServiceResponse> Create(RecipeRequestDto recipeRequestDto)
        {
            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return ServiceResponse.Error(recipeValidationResult.Message);

            var ingredientValidationResult = await _ingredientValidatorService.ValidateIngredientRequestDtos(recipeRequestDto.Ingredients);
            if (!ingredientValidationResult.SuccessStatus) return ServiceResponse.Error(ingredientValidationResult.Message);

            var recipe = await _recipeMapperService.MapCreateRecipeRequestDto(recipeRequestDto);

            try
            {
                var createdRecipe = await _recipeRepository.Create(recipe);
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

            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return ServiceResponse.Error(recipeValidationResult.Message);

            var ingredientValidationResult = await _ingredientValidatorService.ValidateIngredientRequestDtos(recipeRequestDto.Ingredients);
            if (!ingredientValidationResult.SuccessStatus) return ServiceResponse.Error(ingredientValidationResult.Message);

            var updatedRecipe = await _recipeMapperService.MapEditRecipeRequestDto(recipeRequestDto, recipe);
            var mappedIngredients = _ingredientMapperService.MapCreateIngredientRequestDtos(recipeRequestDto.Ingredients, recipeId);

            updatedRecipe.Ingredients = mappedIngredients;

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

        public async Task<ServiceResponse> GetByIdDetailed(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
                if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");
                var getDetailedRecipeResponseDto = _recipeMapperService.MapGetDetailedRecipeResponseDto(recipe);
                return ServiceResponse<GetDetailedRecipeResponseDto>.Success(getDetailedRecipeResponseDto, "Recipe retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipe.");
            }
        }
    }
}