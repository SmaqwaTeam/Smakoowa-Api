namespace Smakoowa_Api.Services.ValidatorServices
{
    public abstract class CommentValidatorService<T> where T : ICommentable
    {
        protected readonly int _maxCommentContentLength;
        protected readonly int _minCommentContentLength;
        protected readonly IApiUserService _apiUserService;
        protected readonly IBaseRepository<T> _commentedRepository;

        public CommentValidatorService(IConfiguration configuration, string commentType, IApiUserService apiUserService,
            IBaseRepository<T> commentedRepository)
        {
            _maxCommentContentLength = int.Parse(configuration.GetSection($"Validation:{commentType}:MaxCommentContentLength").Value);
            _minCommentContentLength = int.Parse(configuration.GetSection($"Validation:{commentType}:MinCommentContentLength").Value);
            _apiUserService = apiUserService;
            _commentedRepository = commentedRepository;
        }

        public async Task<ServiceResponse> ValidateCreateCommentRequestDto(CommentRequestDto commentRequestDto, int commentedId)
        {
            if (!await CheckIfCommentedExists(commentedId))
                return ServiceResponse.Error($"A Item with id: {commentedId} does not exist.", HttpStatusCode.NotFound);

            return ValidateCommentContent(commentRequestDto);
        }

        public async Task<ServiceResponse> ValidateEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment)
        {
            if (!await CheckIfCommentedExists(editedComment.CommentedId))
                return ServiceResponse.Error($"An item with id: {editedComment.CommentedId} does not exist.", HttpStatusCode.NotFound);

            if (!IsCreatorOfComment(editedComment))
                return ServiceResponse.Error($"User is not creator of comment with id {editedComment.Id}.", HttpStatusCode.Unauthorized);

            return ValidateCommentContent(commentRequestDto);
        }

        protected async Task<bool> CheckIfCommentedExists(int commentedId)
        {
            return await _commentedRepository.CheckIfExists(c => c.Id == commentedId);
        }

        protected ServiceResponse ValidateCommentContent(CommentRequestDto commentRequestDto)
        {
            if (commentRequestDto.Content.Length < _minCommentContentLength)
            {
                return ServiceResponse.Error($"Comment content must be min {_minCommentContentLength} characters.", HttpStatusCode.BadRequest);
            }

            if (commentRequestDto.Content.Length > _maxCommentContentLength)
            {
                return ServiceResponse.Error($"Comment content must be max {_maxCommentContentLength} characters.", HttpStatusCode.BadRequest);
            }

            return ServiceResponse.Success();
        }

        protected bool IsCreatorOfComment(Comment comment)
        {
            return comment.CreatorId == _apiUserService.GetCurrentUserId();
        }
    }
}
