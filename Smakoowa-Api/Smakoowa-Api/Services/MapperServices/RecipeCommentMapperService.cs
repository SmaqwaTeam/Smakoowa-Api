namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeCommentMapperService : IRecipeCommentMapperService
    {
        private readonly IMapper _mapper;

        public RecipeCommentMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public RecipeComment MapCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var mappedRecipeComment = _mapper.Map<RecipeComment>(recipeCommentRequestDto);
            mappedRecipeComment.RecipeId = recipeId;
            return mappedRecipeComment;
        }

        public CommentReply MapCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId)
        {
            var mappedCommentReply = _mapper.Map<CommentReply>(commentReplyRequestDto);
            mappedCommentReply.RepliedCommentId = commentId;
            return mappedCommentReply;
        }

        public Comment MapEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment)
        {
            editedComment.Content = commentRequestDto.Content;
            return editedComment;
        }
    }
}
