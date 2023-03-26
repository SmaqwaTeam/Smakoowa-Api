namespace Smakoowa_Api.Models.RequestDtos
{
    public class TagRequestDto : IRequestDto
    {
        public string Name { get; set; }
        public TagType TagType { get; set; }
    }
}
