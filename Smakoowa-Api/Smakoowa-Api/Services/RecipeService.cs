namespace Smakoowa_Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeMapperService _recipeMapperService;
        private readonly IRecipeValidatorService _recipeValidatorService;
        private readonly IHelperService<RecipeService> _helperService;
        private readonly IApiUserService _apiUserService;
        private readonly IApiUserRepository _apiUserRepository;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeMapperService recipeMapperService,
            IRecipeValidatorService recipeValidatorService, IHelperService<RecipeService> helperService, IApiUserService apiUserService,
            IApiUserRepository apiUserRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeMapperService = recipeMapperService;
            _recipeValidatorService = recipeValidatorService;
            _helperService = helperService;
            _apiUserService = apiUserService;
            _apiUserRepository = apiUserRepository;
        }

        public async Task<ServiceResponse> Create(RecipeRequestDto recipeRequestDto)
        {
            var recipeValidationResult = await _recipeValidatorService.ValidateRecipeRequestDto(recipeRequestDto);
            if (!recipeValidationResult.SuccessStatus) return recipeValidationResult;

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

            if (recipe.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.");

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

            if (recipe.CreatorId != _apiUserService.GetCurrentUserId())
                return ServiceResponse.Error($"User isn't the owner of recipe with id: {recipeId}.");

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

        public async Task<ServiceResponse> GetAll(GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => true
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetCurrentUsersRecipes(GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => r.CreatorId == _apiUserService.GetCurrentUserId()
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetRecipesByCategoryId(int categoryId, GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => r.CategoryId == categoryId
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetRecipesByTagIds(List<int> tagIds, GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => r.Tags.Select(t => t.Id).Any(s => tagIds.Contains(s))
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> SearchRecipesByName(string querry, GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => r.Name.ToLower().Contains(querry.ToLower())
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetRecipiesByLikedTags(GetRecipeParameters? getRecipeParameters)
        {
            var userLikedTagIds = (await _apiUserRepository
                .FindByConditionsFirstOrDefault(u => u.Id == _apiUserService.GetCurrentUserId()))
                .TagLikes.Select(t => t.TagId);

            return await GetRecipesByConditions(
                r => r.Tags.Select(t => t.Id).Any(s => userLikedTagIds.Contains(s))
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetLikedRecipies(GetRecipeParameters? getRecipeParameters)
        {
            var userLikedRecipeIds = (await _apiUserRepository
                .FindByConditionsFirstOrDefault(u => u.Id == _apiUserService.GetCurrentUserId()))
                .RecipeLikes.Select(t => t.RecipeId);

            return await GetRecipesByConditions(
                r => userLikedRecipeIds.Contains(r.Id)
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetUserRecipies(int userId, GetRecipeParameters? getRecipeParameters)
        {
            return await GetRecipesByConditions(
                r => r.CreatorId == userId
                , getRecipeParameters);
        }

        private async Task<ServiceResponse> GetRecipesByConditions(Expression<Func<Recipe, bool>> condition, GetRecipeParameters? getRecipeParameters)
        {
            try
            {
                var recipes = await _recipeRepository.FindByConditions(
                    MergeConditions(condition, getRecipeParameters), getRecipeParameters.recipeCount);

                List<RecipeResponseDto> recipeResponseDtos = new();
                foreach (var recipe in recipes) recipeResponseDtos.Add(await _recipeMapperService.MapGetRecipeResponseDto(recipe));

                return ServiceResponse<List<RecipeResponseDto>>.Success(recipeResponseDtos, "Recipes retrieved.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while accessing the recipes.");
            }
        }

        private Expression<Func<Recipe, bool>> MergeConditions(Expression<Func<Recipe, bool>> expresion, GetRecipeParameters? getRecipeParameters)
        {
            return Expression.Lambda<Func<Recipe, bool>>(Expression.AndAlso
                (expresion.Body, Expression.Invoke(IsInDateRangeParameters(getRecipeParameters), expresion.Parameters)), expresion.Parameters);
        }

        private Expression<Func<Recipe, bool>> IsInDateRangeParameters(GetRecipeParameters? getRecipeParameters)
        {
            return r => r.CreatedAt >= getRecipeParameters.startDate && r.CreatedAt <= getRecipeParameters.endDate;
        }
    }
}