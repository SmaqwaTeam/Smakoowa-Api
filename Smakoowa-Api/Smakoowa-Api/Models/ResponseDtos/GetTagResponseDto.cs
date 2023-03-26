namespace Smakoowa_Api.Models.ResponseDtos
{
    public class GetTagResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagType TagType { get; set; }
    }
}
