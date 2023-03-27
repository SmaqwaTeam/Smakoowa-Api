namespace Smakoowa_Api.Models.ResponseDtos
{
    public class GetIngredientResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int Group { get; set; }
    }
}
