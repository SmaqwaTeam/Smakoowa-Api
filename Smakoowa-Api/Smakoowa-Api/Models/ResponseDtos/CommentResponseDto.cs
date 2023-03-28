namespace Smakoowa_Api.Models.ResponseDtos
{
    public class CommentResponseDto : UpdateableResponseDto, IResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
