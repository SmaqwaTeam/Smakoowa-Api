namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeCommentMapperService : CommentMapperService, IRecipeCommentMapperService
    {
        public RecipeCommentMapperService(IMapper mapper) : base(mapper)
        {
        }

        public RecipeComment MapCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId)
        {
            var mappedRecipeComment = _mapper.Map<RecipeComment>(recipeCommentRequestDto);
            mappedRecipeComment.CommentedId = recipeId;
            return mappedRecipeComment;
        }

        public RecipeComment MapEditRecipeCommentRequestDto(RecipeCommentRequestDto commentRequestDto, RecipeComment editedComment)
        {
            return (RecipeComment)MapEditCommentRequestDto(commentRequestDto, editedComment);
        }
    }
}
