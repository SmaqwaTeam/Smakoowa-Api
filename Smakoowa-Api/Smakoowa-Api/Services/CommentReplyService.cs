using Smakoowa_Api.Models.DatabaseModels.Comments;

namespace Smakoowa_Api.Services
{
    public class CommentReplyService : CommentService, ICommentReplyService
    {
        private readonly ICommentReplyRepository _commentReplyRepository;
        private readonly ICommentReplyMapperService _commentReplyMapperService;
        private readonly ICommentReplyValidatorService _commentReplyValidatorService;

        public CommentReplyService(IHelperService<CommentService> helperService, ICommentRepository commentRepository,
            ICommentReplyRepository commentReplyRepository, ICommentReplyMapperService commentReplyMapperService,
            ICommentReplyValidatorService commentReplyValidatorService, IApiUserService apiUserService)
            : base(helperService, commentRepository, apiUserService)
        {
            _commentReplyRepository = commentReplyRepository;
            _commentReplyMapperService = commentReplyMapperService;
            _commentReplyValidatorService = commentReplyValidatorService;
        }

        public async Task<ServiceResponse> AddCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            var commentReplyValidationResult = await _commentReplyValidatorService
                .ValidateCreateCommentReplyRequestDto(commentReplyRequestDto, commentId);

            if (!commentReplyValidationResult.SuccessStatus) return commentReplyValidationResult;

            var mappedCommentReply = _commentReplyMapperService.MapCreateCommentReplyRequestDto(commentReplyRequestDto, commentId);

            return await AddComment(mappedCommentReply);
        }

        public async Task<ServiceResponse> EditCommentReply(CommentReplyRequestDto commentReplyRequestDto, int commentReplyId)
        {
            var editedCommentReply = await _commentReplyRepository.FindByConditionsFirstOrDefault(rc => rc.Id == commentReplyId);
            if (editedCommentReply == null) return ServiceResponse.Error($"Comment reply with id: {commentReplyId} not found.");

            var commentReplyValidationResult = await _commentReplyValidatorService
                .ValidateEditCommentReplyRequestDto(commentReplyRequestDto, editedCommentReply);

            if (!commentReplyValidationResult.SuccessStatus) return commentReplyValidationResult;

            var mappedCommentReply = _commentReplyMapperService.MapEditCommentReplyRequestDto(commentReplyRequestDto, editedCommentReply);

            return await EditComment(mappedCommentReply);
        }

        public async Task<ServiceResponse> DeleteCommentReply(int commentReplyId)
        {
            var commentReplyToDelete = await _commentReplyRepository.FindByConditionsFirstOrDefault(c => c.Id == commentReplyId);
            if (commentReplyToDelete == null) return ServiceResponse.Error($"Comment reply with id: {commentReplyId} not found.");

            if (commentReplyToDelete.CreatorId != _apiUserService.GetCurrentUserId() && !_apiUserService.CurrentUserIsAdmin())
                return ServiceResponse.Error($"User isn't the owner of comment with id: {commentReplyId}.");

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
