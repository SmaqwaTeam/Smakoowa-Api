namespace Smakoowa_Api.Models.ResponseDtos
{
    public class RecipeCommentResponseDto : UpdateableResponseDto, IResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; }

        //public virtual List<CommentReply>? CommentReplies { get; set; }
        //public virtual List<RecipeCommentLike>? RecipeCommentLikes { get; set; }
    }
}
