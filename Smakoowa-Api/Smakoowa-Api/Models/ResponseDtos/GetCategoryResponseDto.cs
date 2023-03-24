namespace Smakoowa_Api.Models.ResponseDtos
{
    public class GetCategoryResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
