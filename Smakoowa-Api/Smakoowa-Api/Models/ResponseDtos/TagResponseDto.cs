namespace Smakoowa_Api.Models.ResponseDtos
{
    public class TagResponseDto : IResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagType TagType { get; set; }
        public int LikeCount { get; set; }
    }
}
