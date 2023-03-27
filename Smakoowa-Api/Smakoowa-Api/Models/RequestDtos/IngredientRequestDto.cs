namespace Smakoowa_Api.Models.RequestDtos
{
    public class IngredientRequestDto : IRequestDto
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Group { get; set; }
    }
}
