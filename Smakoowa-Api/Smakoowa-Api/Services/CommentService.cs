namespace Smakoowa_Api.Services
{
    public abstract class CommentService
    {
        protected readonly IHelperService<CommentService> _helperService;
        protected readonly IApiUserService _apiUserService;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IHelperService<CommentService> helperService, ICommentRepository commentRepository, IApiUserService apiUserService)
        {
            _helperService = helperService;
            _commentRepository = commentRepository;
            _apiUserService = apiUserService;
        }

        protected async Task<ServiceResponse> AddComment(Comment addedComment)
        {
            try
            {
                await _commentRepository.Create(addedComment);
                return ServiceResponse.Success("Comment created.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while creating a comment.");
            }
        }

        protected async Task<ServiceResponse> EditComment(Comment editedComment)
        {
            try
            {
                await _commentRepository.Edit(editedComment);
                return ServiceResponse.Success("Comment edited.");
            }
            catch (Exception ex)
            {
                return _helperService.HandleException(ex, "Something went wrong while editing a comment.");
            }
        }
    }
}
