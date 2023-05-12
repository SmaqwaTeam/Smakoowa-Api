namespace Smakoowa_Api.Services.Interfaces.MapperServices
{
    public interface ICommentMapperService
    {
        public Comment MapCreateCommentRequestDto(CommentRequestDto commentRequestDto, int commentedId);
        public Comment MapEditCommentRequestDto(CommentRequestDto commentRequestDto, Comment editedComment);
    }
}
