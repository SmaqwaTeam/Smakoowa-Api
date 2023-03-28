namespace Smakoowa_Api.Models.ResponseDtos
{
    public class DetailedRecipeResponseDto : RecipeResponseDto
    {
        public List<IngredientResponseDto> Ingredients { get; set; }
        public List<InstructionResponseDto> Instructions { get; set; }
        public List<RecipeCommentResponseDto> RecipeComments { get; set; }
    }
}
