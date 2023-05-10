namespace Smakoowa_Api.Services.ValidatorServices
{
    public class CommentReplyValidatorService : CommentValidatorService, ICommentReplyValidatorService
    {
        private static readonly string commentType = "CommentReply";
        private readonly IRecipeCommentRepository _recipeCommentRepository;

        public CommentReplyValidatorService(IConfiguration configuration, IApiUserService apiUserService,
            IRecipeCommentRepository recipeCommentRepository)
            : base(configuration, commentType, apiUserService)
        {
            _recipeCommentRepository = recipeCommentRepository;
        }

        public async Task<ServiceResponse> ValidateCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            if (!await CheckIfRepliedCommentExists(commentId))
                return ServiceResponse.Error($"A comment with id: {commentId} does not exist.");

            return ValidateCommentContent(commentReplyRequestDto);
        }

        public async Task<ServiceResponse> ValidateEditCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, CommentReply editedCommentReply)
        {
            if (!await CheckIfRepliedCommentExists(editedCommentReply.RepliedCommentId))
                return ServiceResponse.Error($"A comment with id: {editedCommentReply.RepliedCommentId} does not exist.");

            if (!IsCreatorOfComment(editedCommentReply))
                return ServiceResponse.Error($"User is not creator of comment reply with id {editedCommentReply.Id}.");

            return ValidateCommentContent(commentReplyRequestDto);
        }

        private async Task<bool> CheckIfRepliedCommentExists(int recipeCommentId)
        {
            return await _recipeCommentRepository.CheckIfExists(c => c.Id == recipeCommentId);
        }
    }
}
