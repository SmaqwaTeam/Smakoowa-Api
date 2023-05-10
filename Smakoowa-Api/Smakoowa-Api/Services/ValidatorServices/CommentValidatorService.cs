namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class CommentValidatorService
    {
        private readonly int MaxCommentContentLength;
        private readonly int MinCommentContentLength;
        private readonly IApiUserService _apiUserService;

        public CommentValidatorService(IConfiguration configuration, string commentType, IApiUserService apiUserService)
        {
            MaxCommentContentLength = int.Parse(configuration.GetSection($"Validation:{commentType}:MaxCommentContentLength").Value);
            MinCommentContentLength = int.Parse(configuration.GetSection($"Validation:{commentType}:MinCommentContentLength").Value);
            _apiUserService = apiUserService;
        }

        protected ServiceResponse ValidateCommentContent(CommentRequestDto commentRequestDto)
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

        protected bool IsCreatorOfComment(Comment comment)
        {
            return comment.CreatorId == _apiUserService.GetCurrentUserId();
        }
    }
}
