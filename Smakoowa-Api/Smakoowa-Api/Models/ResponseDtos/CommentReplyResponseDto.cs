namespace Smakoowa_Api.Models.ResponseDtos
{
    public class CommentReplyResponseDto : CommentResponseDto
    {
        public List<LikeResponseDto> Likes { get; set; }
    }
}
