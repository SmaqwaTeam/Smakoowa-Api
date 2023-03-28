namespace Smakoowa_Api.Models.RequestDtos
{
    public abstract class CommentRequestDto : IRequestDto
    {
        public string Content { get; set; }
    }
}
