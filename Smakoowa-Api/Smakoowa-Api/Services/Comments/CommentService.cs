namespace Smakoowa_Api.Services.Comments
{
    public abstract class CommentService<T> where T : Comment
    {
        protected readonly IHelperService<CommentService<T>> _helperService;
        protected readonly IApiUserService _apiUserService;
        protected readonly IBaseRepository<T> _commentRepository;
        protected readonly ICommentValidatorService _commentValidatorService;
        protected readonly ICommentMapperService _commentMapperService;

        protected CommentService(ICommentMapperService commentMapperService, ICommentValidatorService commentValidatorService,
            IBaseRepository<T> commentRepository, IApiUserService apiUserService, IHelperService<CommentService<T>> helperService)
        {
            _commentMapperService = commentMapperService;
            _commentValidatorService = commentValidatorService;
            _commentRepository = commentRepository;
            _apiUserService = apiUserService;
            _helperService = helperService;
        }

        public async Task<ServiceResponse> AddComment(CommentRequestDto commentRequestDto, int commentedId)
        {
            var validationResult = await _commentValidatorService.ValidateCreateCommentRequestDto(commentRequestDto, commentedId);
            if (!validationResult.SuccessStatus) return validationResult;

            var mappedComment = _commentMapperService.MapCreateCommentRequestDto(commentRequestDto, commentedId);

            try
            {
                await _commentRepository.Create((T)mappedComment);
                return ServiceResponse.Success("Comment created.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a comment.");
            }
        }

        public async Task<ServiceResponse> EditComment(CommentRequestDto commentRequestDto, int commentId)
        {
            var editedComment = await _commentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == commentId);
            if (editedComment == null) return ServiceResponse.Error($"Comment with id: {commentId} not found.", HttpStatusCode.NotFound);

            var validationResult = await _commentValidatorService.ValidateEditCommentRequestDto(commentRequestDto, editedComment);
            if (!validationResult.SuccessStatus) return validationResult;

            var mappedComment = _commentMapperService.MapEditCommentRequestDto(commentRequestDto, editedComment);

            try
            {
                await _commentRepository.Edit((T)mappedComment);
                return ServiceResponse.Success("Comment edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteComment(int commentId)
        {
            var commentToDelete = await _commentRepository.FindByConditionsFirstOrDefault(c => c.Id == commentId);
            if (commentToDelete == null) return ServiceResponse.Error($"Comment with id: {commentId} not found.", HttpStatusCode.NotFound);

            if (commentToDelete.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
                return ServiceResponse.Error($"User isn't the owner of comment with id: {commentId}.", HttpStatusCode.Unauthorized);

            try
            {
                await _commentRepository.Delete(commentToDelete);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }
    }
}
