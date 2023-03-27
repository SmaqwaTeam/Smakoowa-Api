namespace Smakoowa_Api.Models.RequestDtos
{
    public class InstructionRequestDto : IRequestDto
    {
        public string Content { get; set; }
        public int Position { get; set; }
    }
}
