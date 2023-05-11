namespace Smakoowa_Api.Services
{
    public class CommentReplyLikeService : LikeService, ICommentReplyLikeService
    {
        private readonly ICommentReplyLikeRepository _commentReplyLikeRepository;

        public CommentReplyLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService, IApiUserService apiUserService,
            ILikeValidatorService likeValidatorService, ICommentReplyLikeRepository commentReplyLikeRepository)
            : base(likeRepository, helperService, apiUserService, likeValidatorService)
        {
            _commentReplyLikeRepository = commentReplyLikeRepository;
        }

        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            var commentReplyLikeValidationResult = await _likeValidatorService.ValidateCommentReplyLike(commentReplyId);
            if (!commentReplyLikeValidationResult.SuccessStatus) return commentReplyLikeValidationResult;

            var commentReplyLike = new CommentReplyLike { CommentReplyId = commentReplyId, LikeableType = LikeableType.CommentReply };
            return await AddLike(commentReplyLike);
        }

        public async Task<ServiceResponse> RemoveCommentReplyLike(int commentReplyId)
        {
            var likeToRemove = await _commentReplyLikeRepository.FindByConditionsFirstOrDefault(
                c => c.LikedCommentReply.Id == commentReplyId
                && c.CreatorId == _apiUserService.GetCurrentUserId());

            if (likeToRemove == null) return ServiceResponse.Error($"Like of comment reply with id: {commentReplyId} not found.");

            return await RemoveLike(likeToRemove);
        }

        public async Task<int> GetCommentReplyLikeCount(int commentReplyId)
        {
            return (await _commentReplyLikeRepository.FindByConditions(crl => crl.CommentReplyId == commentReplyId)).Count();
        }
    }
}
