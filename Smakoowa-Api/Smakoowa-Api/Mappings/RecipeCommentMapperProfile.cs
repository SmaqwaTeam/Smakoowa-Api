namespace Smakoowa_Api.Mappings
{
    public class RecipeCommentMapperProfile : Profile
    {
        public RecipeCommentMapperProfile()
        {
            CreateMap<RecipeCommentRequestDto, RecipeComment>();
            CreateMap<RecipeComment, RecipeCommentResponseDto>();
        }
    }
}