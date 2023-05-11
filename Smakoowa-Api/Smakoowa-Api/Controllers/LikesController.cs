﻿namespace Smakoowa_Api.Controllers
{
    [JwtAuthorize("User", "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IRecipeLikeService _recipeLikeService;
        private readonly IRecipeCommentLikeService _recipeCommentLikeService;
        private readonly ICommentReplyLikeService _commentReplyLikeService;
        private readonly ITagLikeService _tagLikeService;

        public LikesController(ICommentReplyLikeService commentReplyLikeService, IRecipeCommentLikeService recipeCommentLikeService,
            IRecipeLikeService recipeLikeService, ITagLikeService tagLikeService)
        {
            _commentReplyLikeService = commentReplyLikeService;
            _recipeCommentLikeService = recipeCommentLikeService;
            _recipeLikeService = recipeLikeService;
            _tagLikeService = tagLikeService;
        }

        [HttpPost("AddRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> AddRecipeLike(int recipeId)
        {
            return await _recipeLikeService.AddRecipeLike(recipeId);
        }

        [HttpPost("AddRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> AddRecipeCommentLike(int recipeCommentId)
        {
            return await _recipeCommentLikeService.AddRecipeCommentLike(recipeCommentId);
        }

        [HttpPost("AddCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> AddCommentReplyLike(int commentReplyId)
        {
            return await _commentReplyLikeService.AddCommentReplyLike(commentReplyId);
        }

        [HttpPost("AddTagLike/{tagId}")]
        public async Task<ServiceResponse> AddTagLike(int tagId)
        {
            return await _tagLikeService.AddTagLike(tagId);
        }

        [HttpDelete("RemoveRecipeLike/{recipeId}")]
        public async Task<ServiceResponse> RemoveRecipeLike(int recipeId)
        {
            return await _recipeLikeService.RemoveRecipeLike(recipeId);
        }

        [HttpDelete("RemoveRecipeCommentLike/{recipeCommentId}")]
        public async Task<ServiceResponse> RemoveRecipeCommentLike(int recipeCommentId)
        {
            return await _recipeCommentLikeService.RemoveRecipeCommentLike(recipeCommentId);
        }

        [HttpDelete("RemoveCommentReplyLike/{commentReplyId}")]
        public async Task<ServiceResponse> RemoveCommentReplyLike(int commentReplyId)
        {
            return await _commentReplyLikeService.RemoveCommentReplyLike(commentReplyId);
        }

        [HttpDelete("RemoveTagLike/{tagId}")]
        public async Task<ServiceResponse> RemoveTagLike(int tagId)
        {
            return await _tagLikeService.RemoveTagLike(tagId);
        }
    }
}
