namespace Smakoowa_Api.Services
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
                return ServiceResponse.Success("Comment created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a comment.");
            }
        }

        public async Task<ServiceResponse> EditComment(CommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            var editedRecipeComment = await _commentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (editedRecipeComment == null) return ServiceResponse.Error($"Comment with id: {recipeCommentId} not found.");

            var recipeCommentValidationResult = await _commentValidatorService
                .ValidateEditCommentRequestDto(recipeCommentRequestDto, editedRecipeComment);

            if (!recipeCommentValidationResult.SuccessStatus) return recipeCommentValidationResult;

            var mappedRecipeComment = _commentMapperService.MapEditCommentRequestDto(recipeCommentRequestDto, editedRecipeComment);

            try
            {
                await _commentRepository.Edit((T)mappedRecipeComment);
                return ServiceResponse.Success("Comment edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteComment(int recipeCommentId)
        {
            var recipeCommentToDelete = await _commentRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeCommentId);
            if (recipeCommentToDelete == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            if (recipeCommentToDelete.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
                return ServiceResponse.Error($"User isn't the owner of comment with id: {recipeCommentId}.");

            try
            {
                await _commentRepository.Delete(recipeCommentToDelete);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }
    }
}
