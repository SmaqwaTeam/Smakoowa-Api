namespace Smakoowa_Api.Models.ResponseDtos
{
    public class RecipeCommentResponseDto : CommentResponseDto
    {
        public List<CommentReplyResponseDto>? CommentReplies { get; set; }
        //public virtual List<RecipeCommentLike>? RecipeCommentLikes { get; set; }
    }
}
