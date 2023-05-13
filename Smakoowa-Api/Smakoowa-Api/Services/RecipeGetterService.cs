namespace Smakoowa_Api.Services
{
    public class RecipeGetterService : IRecipeGetterService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IApiUserRepository _apiUserRepository;
        private readonly IHelperService<RecipeGetterService> _helperService;
        private readonly IRecipeMapperService _recipeMapperService;
        private readonly IApiUserService _apiUserService;

        public RecipeGetterService(IApiUserService apiUserService, IRecipeMapperService recipeMapperService,
            IHelperService<RecipeGetterService> helperService, IApiUserRepository apiUserRepository, IRecipeRepository recipeRepository)
        {
            _apiUserService = apiUserService;
            _recipeMapperService = recipeMapperService;
            _helperService = helperService;
            _apiUserRepository = apiUserRepository;
            _recipeRepository = recipeRepository;
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
                .TagLikes.Select(t => t.LikedId);

            return await GetRecipesByConditions(
                r => r.Tags.Select(t => t.Id).Any(s => userLikedTagIds.Contains(s))
                , getRecipeParameters);
        }

        public async Task<ServiceResponse> GetLikedRecipies(GetRecipeParameters? getRecipeParameters)
        {
            var userLikedRecipeIds = (await _apiUserRepository
                .FindByConditionsFirstOrDefault(u => u.Id == _apiUserService.GetCurrentUserId()))
                .RecipeLikes.Select(t => t.LikedId);

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
