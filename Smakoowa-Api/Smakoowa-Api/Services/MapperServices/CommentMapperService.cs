namespace Smakoowa_Api.Services.MapperServices
{
    public abstract class CommentMapperService
    {
        protected readonly IMapper _mapper;

        public CommentMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Comment MapEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment)
        {
            editedComment.Content = commentRequestDto.Content;
            return editedComment;
        }
    }
}
