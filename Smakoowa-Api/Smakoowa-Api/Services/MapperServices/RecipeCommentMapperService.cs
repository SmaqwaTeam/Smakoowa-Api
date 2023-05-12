namespace Smakoowa_Api.Services.MapperServices
{
    public class RecipeCommentMapperService : CommentMapperService<RecipeComment>, IRecipeCommentMapperService
    {
        public RecipeCommentMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
