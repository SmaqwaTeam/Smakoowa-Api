namespace Smakoowa_Api.Models.ResponseDtos
{
    public class RecipeCommentResponseDto : CommentResponseDto
    {
        public List<CommentReplyResponseDto>? CommentReplies { get; set; }
        public List<LikeResponseDto> Likes { get; set; }
    }
}
