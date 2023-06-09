﻿namespace Smakoowa_Api.Services.Interfaces.Comments
{
    public interface ICommentService
    {
        public Task<ServiceResponse> AddComment(CommentRequestDto commentRequestDto, int commentedId);
        public Task<ServiceResponse> EditComment(CommentRequestDto commentRequestDto, int commentedId);
        public Task<ServiceResponse> DeleteComment(int recipeCommentId);
    }
}
