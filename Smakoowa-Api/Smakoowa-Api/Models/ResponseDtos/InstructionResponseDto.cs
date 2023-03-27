namespace Smakoowa_Api.Models.ResponseDtos
{
    public class InstructionResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Position { get; set; }
        public string? ImageUrl { get; set; }
    }
}
