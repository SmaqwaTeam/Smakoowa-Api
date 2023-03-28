namespace Smakoowa_Api.Services.ValidatorServices
{
    public class RecipeCommentValidatorService : IRecipeCommentValidatorService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly int MaxContentLength;
        private readonly int MinContentLength;

        public RecipeCommentValidatorService(IConfiguration configuration, IRecipeRepository recipeRepository)
        {
            MaxContentLength = int.Parse(configuration.GetSection($"Validation:RecipeComment:MaxContentLength").Value);
            MinContentLength = int.Parse(configuration.GetSection($"Validation:RecipeComment:MinContentLength").Value);
            _recipeRepository = recipeRepository;
        }

        public async Task<ServiceResponse> ValidateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            if (!await _recipeRepository.CheckIfExists(r => r.Id == recipeId))
            {
                return ServiceResponse.Error($"A recipe with id: {recipeId} does not exist.");
            }

            if (recipeCommentRequestDto.Content.Length < MinContentLength)
            {
                return ServiceResponse.Error($"Recipe comment content must be min {MinContentLength} characters.");
            }

            if (recipeCommentRequestDto.Content.Length > MaxContentLength)
            {
                return ServiceResponse.Error($"Recipe comment content must be max {MaxContentLength} characters.");
            }

            return ServiceResponse.Success();
        }
    }
}
