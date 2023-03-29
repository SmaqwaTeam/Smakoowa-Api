﻿namespace Smakoowa_Api.Models.DatabaseModels.Comments
{
    public class CommentReply : Comment, ILikeable
    {
        public int RepliedCommentId { get; set; }
        public virtual RecipeComment RepliedComment { get; set; }

        public virtual List<CommentReplyLike>? Likes { get; set; }
    }
}
