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

        public RecipeComment MapEditRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, RecipeComment editedRecipeComment)
        {
            editedRecipeComment.Content = recipeCommentRequestDto.Content;
            return editedRecipeComment;
        }
    }
}
