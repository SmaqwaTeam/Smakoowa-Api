namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CommentValidatorService : ICommentValidatorService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly int MaxCommentContentLength;
        private readonly int MinCommentContentLength;

        public CommentValidatorService(IConfiguration configuration, IRecipeRepository recipeRepository, IRecipeCommentRepository recipeCommentRepository)
        {
            MaxCommentContentLength = int.Parse(configuration.GetSection($"Validation:Comment:MaxCommentContentLength").Value);
            MinCommentContentLength = int.Parse(configuration.GetSection($"Validation:Comment:MinCommentContentLength").Value);
            _recipeRepository = recipeRepository;
            _recipeCommentRepository = recipeCommentRepository;
        }

        public async Task<ServiceResponse> ValidateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            if (!await _recipeRepository.CheckIfExists(r => r.Id == recipeId))
            {
                return ServiceResponse.Error($"A recipe with id: {recipeId} does not exist.");
            }

            return ValidateCommentContent(recipeCommentRequestDto);
        }

        public async Task<ServiceResponse> ValidateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            if (!await _recipeCommentRepository.CheckIfExists(c => c.Id == commentId))
            {
                return ServiceResponse.Error($"A comment with id: {commentId} does not exist.");
            }

            return ValidateCommentContent(commentReplyRequestDto);
        }

        private ServiceResponse ValidateCommentContent(CommentRequestDto commentRequestDto)
        {
            if (commentRequestDto.Content.Length < MinCommentContentLength)
            {
                return ServiceResponse.Error($"Comment content must be min {MinCommentContentLength} characters.");
            }

            if (commentRequestDto.Content.Length > MaxCommentContentLength)
            {
                return ServiceResponse.Error($"Comment content must be max {MaxCommentContentLength} characters.");
            }

            return ServiceResponse.Success();
        }
    }
}
