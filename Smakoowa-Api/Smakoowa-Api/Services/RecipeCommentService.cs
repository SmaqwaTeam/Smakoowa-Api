namespace Smakoowa_Api.Services
{
    public class RecipeCommentService : IRecipeCommentService
    {
        private readonly IRecipeCommentRepository _recipeCommentRepository;
        private readonly IRecipeCommentMapperService _recipeCommentMapperService;
        private readonly IRecipeCommentValidatorService _recipeCommentValidatorService;
        private readonly ICommentReplyRepository _commentReplyRepository;
        private readonly IHelperService<RecipeCommentService> _helperService;
        private readonly IBaseRepository<Comment> _commentRepository;

        public RecipeCommentService(IRecipeCommentValidatorService recipeCommentValidatorService, IRecipeCommentMapperService recipeCommentMapperService,
            IRecipeCommentRepository recipeCommentRepository, IHelperService<RecipeCommentService> helperService, IBaseRepository<Comment> commentRepository,
            ICommentReplyRepository commentReplyRepository)
        {
            _recipeCommentValidatorService = recipeCommentValidatorService;
            _recipeCommentMapperService = recipeCommentMapperService;
            _recipeCommentRepository = recipeCommentRepository;
            _helperService = helperService;
            _commentRepository = commentRepository;
            _commentReplyRepository = commentReplyRepository;
        }

        public async Task<ServiceResponse> AddRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var recipeCommentValidationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);
            if (!recipeCommentValidationResult.SuccessStatus) return ServiceResponse.Error(recipeCommentValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapCreateRecipeCommentRequestDto(recipeCommentRequestDto, recipeId);

            return await AddCommentToDatabase(mappedRecipeComment);
        }

        public async Task<ServiceResponse> AddCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            var commentReplyValidationResult = await _recipeCommentValidatorService.ValidateCommentReplyRequestDto(commentReplyRequestDto, commentId);
            if (!commentReplyValidationResult.SuccessStatus) return ServiceResponse.Error(commentReplyValidationResult.Message);

            var mappedRecipeComment = _recipeCommentMapperService.MapCreateCommentReplyRequestDto(commentReplyRequestDto, commentId);

            return await AddCommentToDatabase(mappedRecipeComment);
        }

        private async Task<ServiceResponse> AddCommentToDatabase(Comment mappedRecipeComment)
        {
            try
            {
                await _commentRepository.Create(mappedRecipeComment);
                return ServiceResponse.Success("Comment created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteRecipeComment(int recipeCommentId)
        {
            var comment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(c => c.Id == recipeCommentId);
            if (comment == null) return ServiceResponse.Error($"Comment with id: {recipeCommentId} not found.");

            try
            {
                await _recipeCommentRepository.Delete(comment);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }

        public async Task<ServiceResponse> DeleteCommentReply(int commentReplyId)
        {
            var comment = await _commentReplyRepository.FindByConditionsFirstOrDefault(c => c.Id == commentReplyId);
            if (comment == null) return ServiceResponse.Error($"Comment with id: {commentReplyId} not found.");

            try
            {
                await _commentRepository.Delete(comment);
                return ServiceResponse.Success("Comment deleted.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while deleting a comment.");
            }
        }

        public async Task<ServiceResponse> EditRecipeComment(RecipeCommentRequestDto recipeCommentRequestDto, int recipeCommentId)
        {
            var editedRecipeComment = await _recipeCommentRepository.FindByConditionsFirstOrDefault(rc => rc.Id == recipeCommentId);
            if (editedRecipeComment == null) return ServiceResponse.Error($"Recipe comment with id: {recipeCommentId} not found.");

            var validationResult = await _recipeCommentValidatorService.ValidateRecipeCommentRequestDto(recipeCommentRequestDto, editedRecipeComment.RecipeId);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);
            return await EditComment(recipeCommentRequestDto, editedRecipeComment);
        }

        public async Task<ServiceResponse> EditCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            var editedCommentReply = await _commentReplyRepository.FindByConditionsFirstOrDefault(rc => rc.Id == commentReplyId);
            if (editedCommentReply == null) return ServiceResponse.Error($"Comment reply with id: {commentReplyId} not found.");

            var validationResult = await _recipeCommentValidatorService.ValidateCommentReplyRequestDto(commentReplyRequestDto, editedCommentReply.RepliedCommentId);
            if (!validationResult.SuccessStatus) return ServiceResponse.Error(validationResult.Message);
            return await EditComment(commentReplyRequestDto, editedCommentReply);
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
    }
}
