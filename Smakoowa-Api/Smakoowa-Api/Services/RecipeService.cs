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
        private readonly IInstructionValidatorService _instructionValidatorService;
        private readonly IInstructionMapperService _instructionMapperService;
        private readonly IApiUserService _apiUserService;
        private readonly IApiUserRepository _apiUserRepository;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeMapperService recipeMapperService, IRecipeValidatorService recipeValidatorService,
            IHelperService<RecipeService> helperService, IIngredientValidatorService ingredientValidatorService, IIngredientMapperService ingredientMapperService,
            IInstructionValidatorService instructionValidatorService, IInstructionMapperService instructionMapperService, IApiUserService apiUserService, IApiUserRepository apiUserRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeMapperService = recipeMapperService;
            _recipeValidatorService = recipeValidatorService;
            _helperService = helperService;
            _ingredientValidatorService = ingredientValidatorService;
            _ingredientMapperService = ingredientMapperService;
            _instructionValidatorService = instructionValidatorService;
            _instructionMapperService = instructionMapperService;
            _apiUserService = apiUserService;
            _apiUserRepository = apiUserRepository;
        }

        public async Task<ServiceResponse> Create(RecipeRequestDto recipeRequestDto)
        {
            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return ServiceResponse.Error(recipeValidationResult.Message);

            var ingredientValidationResult = await _ingredientValidatorService.ValidateIngredientRequestDtos(recipeRequestDto.Ingredients);
            if (!ingredientValidationResult.SuccessStatus) return ServiceResponse.Error(ingredientValidationResult.Message);

            var instructionValidationResult = await _instructionValidatorService.ValidateInstructionRequestDtos(recipeRequestDto.Instructions);
            if (!instructionValidationResult.SuccessStatus) return ServiceResponse.Error(instructionValidationResult.Message);

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

            var instructionValidationResult = await _instructionValidatorService.ValidateInstructionRequestDtos(recipeRequestDto.Instructions);
            if (!instructionValidationResult.SuccessStatus) return ServiceResponse.Error(instructionValidationResult.Message);

            var updatedRecipe = await _recipeMapperService.MapEditRecipeRequestDto(recipeRequestDto, recipe);
            var mappedIngredients = _ingredientMapperService.MapCreateIngredientRequestDtos(recipeRequestDto.Ingredients, recipeId);
            var mappedInstructions = _instructionMapperService.MapCreateInstructionRequestDtos(recipeRequestDto.Instructions, recipeId);

            updatedRecipe.Ingredients = mappedIngredients;
            updatedRecipe.Instructions = mappedInstructions;

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

        public async Task<ServiceResponse> GetByIdDetailed(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
                if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");
                var getDetailedRecipeResponseDto = await _recipeMapperService.MapGetDetailedRecipeResponseDto(recipe);
                return ServiceResponse<DetailedRecipeResponseDto>.Success(getDetailedRecipeResponseDto, "Recipe retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipe.");
            }
        }

        public async Task<ServiceResponse> GetById(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeId);
                if (recipe == null) return ServiceResponse.Error($"Recipe with id: {recipeId} not found.");
                var getRecipeResponseDto = await _recipeMapperService.MapGetRecipeResponseDto(recipe);
                return ServiceResponse<RecipeResponseDto>.Success(getRecipeResponseDto, "Recipe retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipe.");
            }
        }

        public async Task<ServiceResponse> GetAll()
        {
            return await GetRecipesByConditions(c => true);
        }

        public async Task<ServiceResponse> GetCurrentUsersRecipes()
        {
            return await GetRecipesByConditions(r => r.CreatorId == _apiUserService.GetCurrentUserId());
        }

        public async Task<ServiceResponse> GetRecipesByCategoryId(int categoryId)
        {
            return await GetRecipesByConditions(r => r.CategoryId == categoryId);
        }

        public async Task<ServiceResponse> GetRecipesByTagIds(List<int> tagIds)
        {
            return await GetRecipesByConditions(c => c.Tags.Select(t => t.Id).Any(s => tagIds.Contains(s)));
        }

        public async Task<ServiceResponse> SearchRecipesByName(string querry)
        {
            return await GetRecipesByConditions(r => r.Name.ToLower().Contains(querry.ToLower()));
        }

        public async Task<ServiceResponse> GetRecipiesByLikedTags()
        {
            var userLikedTagIds = (await _apiUserRepository
                .FindByConditionsFirstOrDefault(u => u.Id == _apiUserService.GetCurrentUserId()))
                .TagLikes.Select(t => t.TagId);

            return await GetRecipesByConditions(c => c.Tags.Select(t => t.Id).Any(s => userLikedTagIds.Contains(s)));
        }

        public async Task<ServiceResponse> GetLikedRecipies()
        {
            var userLikedRecipeIds = (await _apiUserRepository
                .FindByConditionsFirstOrDefault(u => u.Id == _apiUserService.GetCurrentUserId()))
                .RecipeLikes.Select(t => t.RecipeId);

            return await GetRecipesByConditions(c => userLikedRecipeIds.Contains(c.Id));
        }

        public async Task<ServiceResponse> GetUserRecipies(int userId)
        {
            return await GetRecipesByConditions(r => r.CreatorId == userId);
        }

        private async Task<ServiceResponse> GetRecipesByConditions(Expression<Func<Recipe, bool>> expresion)
        {
            try
            {
                var recipes = await _recipeRepository.FindByConditions(expresion);

                List<RecipeResponseDto> recipeResponseDtos = new();
                foreach (var recipe in recipes) recipeResponseDtos.Add(await _recipeMapperService.MapGetRecipeResponseDto(recipe));

                return ServiceResponse<List<RecipeResponseDto>>.Success(recipeResponseDtos, "Recipes retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipes.");
            }
        }
    }
}