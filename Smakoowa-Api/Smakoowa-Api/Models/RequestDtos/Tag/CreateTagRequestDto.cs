namespace Smakoowa_Api.Models.RequestDtos.Tag
{
    public class CreateTagRequestDto : IRequestDto
    {
        public string Name { get; set; }
        public TagType TagType { get; set; }
    }
}
