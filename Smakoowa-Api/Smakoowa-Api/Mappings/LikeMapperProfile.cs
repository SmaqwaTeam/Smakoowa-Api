namespace Smakoowa_Api.Mappings
{
    public class LikeMapperProfile : Profile
    {
        public LikeMapperProfile()
        {
            CreateMap<RecipeLike, LikeResponseDto>();
            CreateMap<RecipeCommentLike, LikeResponseDto>();
            CreateMap<CommentReplyLike, LikeResponseDto>();
        }
    }
}

