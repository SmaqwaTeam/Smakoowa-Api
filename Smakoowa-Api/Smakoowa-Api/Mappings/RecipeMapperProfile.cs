namespace Smakoowa_Api.Mappings
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeRequestDto, Recipe>();
            CreateMap<Recipe, RecipeResponseDto>()
                .AfterMap((src, dest) => dest.TagIds = src.Tags?.Select(t => t.Id).ToList());
            CreateMap<Recipe, DetailedRecipeResponseDto>()
                .AfterMap((src, dest) => dest.TagIds = src.Tags?.Select(t => t.Id).ToList());
        }
    }
}
