namespace Smakoowa_Api.Services
{
    public class CommentReplyLikeService : LikeService, ICommentReplyLikeService
    {
        private readonly ILikeValidatorService _likeValidatorService;
        private readonly IBaseRepository<CommentReplyLike> _commentReplyLikeRepository;

        public CommentReplyLikeService(ILikeRepository likeRepository, IHelperService<LikeService> helperService,
            ILikeValidatorService likeValidatorService, IBaseRepository<CommentReplyLike> commentReplyLikeRepository)
            : base(likeRepository, helperService)
        {
            _likeValidatorService = likeValidatorService;
            _commentReplyLikeRepository = commentReplyLikeRepository;
        }

        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            var commentReplyLikeValidationResult = await _likeValidatorService.ValidateCommentReplyLike(commentReplyId);
            if (!commentReplyLikeValidationResult.SuccessStatus) return commentReplyLikeValidationResult;

            var commentReplyLike = new CommentReplyLike { CommentReplyId = commentReplyId, LikeableType = LikeableType.CommentReply };
            return await AddLike(commentReplyLike);
        }

        public async Task<ServiceResponse> RemoveCommentReplyLike(int likeId)
        {
            var likeToRemove = await _commentReplyLikeRepository.FindByConditionsFirstOrDefault(c => c.Id == likeId);
            if (likeToRemove == null) return ServiceResponse.Error($"Like with id: {likeId} not found.");
            return await RemoveLike(likeToRemove);
        }
    }
}
