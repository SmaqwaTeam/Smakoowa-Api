namespace Smakoowa_Api.Models.RequestDtos.Tag
{
    public class EditTagRequestDto : IRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagType TagType { get; set; }
    }
}
