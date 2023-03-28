namespace Smakoowa_Api.Mappings
{
    public class CommentReplyMapperProfile : Profile
    {
        public CommentReplyMapperProfile()
        {
            CreateMap<CommentReplyRequestDto, CommentReply>();
            CreateMap<CommentReply, CommentReplyResponseDto>();
        }
    }
}
