namespace Smakoowa_Api.Models.ResponseDtos
{
    public class IngredientResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int Group { get; set; }
    }
}
