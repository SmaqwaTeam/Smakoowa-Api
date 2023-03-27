namespace Smakoowa_Api.Mappings
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeRequestDto, Recipe>();
            CreateMap<Recipe, GetRecipeResponseDto>();
            CreateMap<Recipe, GetDetailedRecipeResponseDto>();
        }
    }
}
