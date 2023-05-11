namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface IRecipeCommentMapperService
    {
        public RecipeComment MapCreateRecipeCommentRequestDto(RecipeCommentRequestDto recipeCommentRequestDto, int recipeId);
        public RecipeComment MapEditRecipeCommentRequestDto(RecipeCommentRequestDto commentRequestDto, RecipeComment editedComment);
    }
}
