namespace Smakoowa_Api.Services.MapperServices
{
    public abstract class CommentMapperService<T> where T : Comment
    {
        protected readonly IMapper _mapper;

        public CommentMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Comment MapCreateCommentRequestDto(CommentRequestDto commentRequestDto, int commentedId)
        {
            var mappedRecipeComment = _mapper.Map<T>(commentRequestDto);
            mappedRecipeComment.CommentedId = commentedId;
            return mappedRecipeComment;
        }

        public Comment MapEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment)
        {
            editedComment.Content = commentRequestDto.Content;
            return editedComment;
        }
    }
}
