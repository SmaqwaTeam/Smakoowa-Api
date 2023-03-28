namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IRecipeCommentMapperService
    {
        public RecipeComment MapCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public CommentReply MapCreateCommentReplyRequestDto(CommentReplyRequestDto commentReplyRequestDto, int commentId);
        public Comment MapEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment);
    }
}
