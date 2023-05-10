namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeCommentValidatorService : CommentValidatorService, IRecipeCommentValidatorService
    {
        private static readonly string commentType = "RecipeComment";
        private readonly IRecipeRepository _recipeRepository;

        public RecipeCommentValidatorService(IConfiguration configuration, IApiUserService apiUserService, IRecipeRepository recipeRepository)
            : base(configuration, commentType, apiUserService)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ServiceResponse> ValidateCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            if (!await CheckIfRecipeExists(recipeId))
                return ServiceResponse.Error($"A recipe with id: {recipeId} does not exist.");

            return ValidateCommentContent(recipeCommentRequestDto);
        }

        public async Task<ServiceResponse> ValidateEditRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, RecipeComment editedRecipeComment)
        {
            if (!await CheckIfRecipeExists(editedRecipeComment.RecipeId))
                return ServiceResponse.Error($"A recipe with id: {editedRecipeComment.RecipeId} does not exist.");

            if (!IsCreatorOfComment(editedRecipeComment))
                return ServiceResponse.Error($"User is not creator of recipe comment with id {editedRecipeComment.Id}.");

            return ValidateCommentContent(recipeCommentRequestDto);
        }

        private async Task<bool> CheckIfRecipeExists(int recipeId)
        {
            return await _recipeRepository.CheckIfExists(c => c.Id == recipeId);
        }
    }
}
