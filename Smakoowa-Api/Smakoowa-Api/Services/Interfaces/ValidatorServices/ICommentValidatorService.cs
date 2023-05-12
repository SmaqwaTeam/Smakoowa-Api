namespace Smakoowa_Api.Services.Interfaces.ValidatorServices
{
    public interface ICommentValidatorService
    {
        public Task<ServiceResponse> ValidateCreateCommentRequestDto(CommentRequestDto commentRequestDto, int commentedId);
        public Task<ServiceResponse> ValidateEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment);
    }
}
