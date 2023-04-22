namespace Smakoowa_Api.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly ICommentMapperService _recipeCommentMapperService;
        private readonly ICommentValidatorService _recipeCommentValidatorService;
        private readonly ICommentReplyRepository _commentReplyRepository;
        private readonly IHelperService<CommentService> _helperService;
        private readonly IBaseRepository<Comment> _commentRepository;
        private readonly IApiUserService _apiUserService;

        public CommentService(ICommentValidatorService recipeCommentValidatorService, ICommentMapperService recipeCommentMapperService,
            IRecipeCommentRepository recipeCommentRepository, IHelperService<CommentService> helperService, IBaseRepository<Comment> commentRepository,
            ICommentReplyRepository commentReplyRepository, IApiUserService apiUserService)
        {
            _recipeCommentValidatorService = recipeCommentValidatorService;
            _recipeCommentMapperService = recipeCommentMapperService;
            _recipeCommentRepository = recipeCommentRepository;
            _helperService = helperService;
            _commentRepository = commentRepository;
            _commentReplyRepository = commentReplyRepository;
            _apiUserService = apiUserService;
        }

        public async Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var recipeCommentValidationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);
            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapCreateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);

            return await AddComment(mappedRecipeComment);
        }

        public async Task<ServiceResponse> AddCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            var commentReplyValidationResult = await _recipeCommentValidatorService.ValidateCommentReplyRequestDto(commentReplyRequestDto, commentId);
            if (!commentReplyValidationResult.SuccessStatus) return ServiceResponse.Error(commentReplyValidationResult.Message);

            var mappedCommentReply = _recipeCommentMapperService.MapCreateCommentReplyRequestDto(commentReplyRequestDto, commentId);

            return await AddComment(mappedCommentReply);
        }

        private async Task<ServiceResponse> AddComment(Comment mappedComment)
        {
            try
            {
                await _commentRepository.Create(mappedComment);
                return ServiceResponse.Success("Comment created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a comment.");
            }
        }

        public async Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            var editedRecipeComment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (editedRecipeComment == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            if (!IsCreatorOfComment(editedRecipeComment))
                return ServiceResponse.Error($"User is not creator of comment reply with id {recipeCommentId}.");

            var recipeCommentValidationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment.RecipeId);
            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);
            return await EditComment(recipeCommentRequestDto, editedRecipeComment);
        }

        public async Task<ServiceResponse> EditCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            var editedCommentReply = await _commentReplyRepository.FindByConditionsFirstOrDefault(rc => rc.Id == commentReplyId);
            if (editedCommentReply == null) return ServiceResponse.Error($"Comment reply with id: {commentReplyId} not found.");

            if (!IsCreatorOfComment(editedCommentReply))
                return ServiceResponse.Error($"User is not creator of comment reply with id {commentReplyId}.");

            var commentReplyValidationResult = await _recipeCommentValidatorService.ValidateCommentReplyRequestDto(commentReplyRequestDto, editedCommentReply.RepliedCommentId);
            if (!commentReplyValidationResult.SuccessStatus) return ServiceResponse.Error(commentReplyValidationResult.Message);
            return await EditComment(commentReplyRequestDto, editedCommentReply);
        }

        private bool IsCreatorOfComment(Comment editedComment)
        {
            return editedComment.CreatorId == _apiUserService.GetCurrentUserId();
        }

        private async Task<ServiceResponse> EditComment(CommentRequestDto commentRequestDto, Comment editedComment)
        {
            var mappedEditedComment = _recipeCommentMapperService.MapEditCommentRequestDto(commentRequestDto, editedComment);

            try
            {
                await _commentRepository.Edit(mappedEditedComment);
                return ServiceResponse.Success("Comment edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            var recipeCommentToDelete = await _recipeCommentRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeCommentId);
            if (recipeCommentToDelete == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            try
            {
                await _recipeCommentRepository.Delete(recipeCommentToDelete);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteCommentReply(int commentReplyId)
        {
            var commentReplyToDelete = await _commentReplyRepository.FindByConditionsFirstOrDefault(c => c.Id == commentReplyId);
            if (commentReplyToDelete == null) return ServiceResponse.Error($"Comment reply with id: {commentReplyId} not found.");

            try
            {
                await _commentReplyRepository.Delete(commentReplyToDelete);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }
    }
}
