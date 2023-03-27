namespace Smakoowa_Api.Models.ResponseDtos
{
    public class GetDetailedRecipeResponseDto : GetRecipeResponseDto
    {
        public List<GetIngredientResponseDto> Ingredients { get; set; }
    }
}
